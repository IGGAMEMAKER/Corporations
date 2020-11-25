using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void AttachToGroup(GameContext context, int parentId, int subsidiaryId) => AttachToGroup(context, Get(context, parentId), Get(context, subsidiaryId));
        public static void AttachToGroup(GameContext context, GameEntity parent, GameEntity daughter)
        {
            // TODO only possible if independent!

            // we cannot attach company to product company
            if (parent.hasProduct)
                return;

            if (daughter.hasProduct)
            {
                var industry = Markets.GetIndustry(daughter.product.Niche, context);

                AddFocusNiche(parent, daughter.product.Niche, context);
                AddFocusIndustry(industry, parent);
            }

            AddOwning(parent, daughter.company.Id);

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

            SetIndependence(daughter, false);
        }

        public static void AddOwning(GameEntity company, int owningCompanyId)
        {
            if (!company.ownings.Holdings.Contains(owningCompanyId))
                company.ownings.Holdings.Add(owningCompanyId);
        }

        public static void RemoveOwning(GameEntity owner, int owningCompanyId)
        {
            if (owner.ownings.Holdings.Contains(owningCompanyId))
                owner.ownings.Holdings.Remove(owningCompanyId);
        }

        public static GameEntity CreateProductAndAttachItToGroup(GameContext gameContext, NicheType nicheType, GameEntity group)
        {
            string name = group.company.Name + " " + Enums.GetFormattedNicheName(nicheType);

            var c = GenerateProductCompany(gameContext, name, nicheType);

            //AttachToGroup(gameContext, group.company.Id, c.company.Id);
            AttachToGroup(gameContext, group, c);

            return c;
        }

        public static void TurnProductToPlayerFlagship(GameEntity company, GameContext Q, NicheType nicheType)
        {
            company.isFlagship = true;
            company.AddChannelExploration(new Dictionary<int, int>(), new List<int>(), 1);

            // give bad positioning initially
            var infos = Marketing.GetAudienceInfos();

            Marketing.AddClients(company, -50, Marketing.GetCoreAudienceId(company));

            var positionings = Markets.GetNichePositionings(nicheType, Q);
            var positioningWorths = positionings.OrderBy(Markets.GetPositioningValue);

            // TODO POSITIONING
            var rand = Random.Range(0, 2);
            var newPositioning = rand < 1 ? 0 : 3; //  positioningWorths.ToArray()[rand].ID;
            // 0 - teens, 3 - old people

            Marketing.ChangePositioning(company, newPositioning);

            Marketing.AddClients(company, 50, Marketing.GetCoreAudienceId(company));

            // give good salary to CEO, so he will not leave company
            var CEO = Humans.Get(Q, Companies.GetCEOId(company));

            var salary = Teams.GetSalaryPerRating(CEO);
            Teams.SetJobOffer(CEO, company, new JobOffer(salary), 0, Q);
        }

    }
}
