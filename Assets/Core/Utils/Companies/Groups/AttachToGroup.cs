using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void AttachToGroup(GameContext context, int parentId, int subsidiaryId)
        {
            // TODO only possible if independent!

            var parent = Get(context, parentId);


            // we cannot attach company to product company
            if (!IsCompanyGroupLike(parent))
                return;

            var daughter = Get(context, subsidiaryId);

            if (daughter.hasProduct)
            {
                var industry = Markets.GetIndustry(daughter.product.Niche, context);

                AddFocusNiche(daughter.product.Niche, parent, context);
                AddFocusIndustry(industry, parent);
            }


            //Debug.Log("Attach " + daughter.company.Name + " to " + parent.company.Name);

            AddOwning(parent, subsidiaryId);
            var shareholders = new Dictionary<int, BlockOfShares>
            {
                {
                    parent.shareholder.Id,
                    new BlockOfShares
                    {
                        amount = 100,
                        InvestorType = InvestorType.Strategic,
                        shareholderLoyalty = 100,

                        Investments = new List<Investment>()
                    }
                }
            };

            if (daughter.hasShareholders)
                daughter.ReplaceShareholders(shareholders);
            else
                daughter.AddShareholders(shareholders);

            daughter.isIndependentCompany = false;
        }

        public static void AddOwning(GameEntity company, int owningCompanyId)
        {
            if (!company.ownings.Holdings.Contains(owningCompanyId))
                company.ownings.Holdings.Add(owningCompanyId);
        }

        public static GameEntity CreateProductAndAttachItToGroup(GameContext gameContext, NicheType nicheType, GameEntity group)
        {
            string name = group.company.Name + " " + Enums.GetFormattedNicheName(nicheType);

            var c = GenerateProductCompany(gameContext, name, nicheType);

            AttachToGroup(gameContext, group.company.Id, c.company.Id);

            return c;
        }

        public static void TurnProductToPlayerFlagship(GameEntity company, GameContext Q, NicheType nicheType)
        {
            company.isFlagship = true;
            company.AddChannelExploration(new Dictionary<int, int>(), new List<int>(), 1);

            // give bad positioning initially
            var infos = Marketing.GetAudienceInfos();

            Marketing.AddClients(company, -50, company.productPositioning.Positioning);

            var positionings = Markets.GetNichePositionings(nicheType, Q);
            var positioningWorths = positionings.OrderBy(Markets.GetPositioningValue);

            var rand = Random.Range(0, 2);
            company.productPositioning.Positioning = rand < 1 ? 0 : 3; //  positioningWorths.ToArray()[rand].ID;

            Marketing.AddClients(company, 50, company.productPositioning.Positioning);

            // give good salary to CEO, so he will not leave company
            var CEO = Humans.Get(Q, Companies.GetCEOId(company));

            var salary = Teams.GetSalaryPerRating(CEO);
            Teams.SetJobOffer(CEO, company, new JobOffer(salary), 0);
        }

    }
}
