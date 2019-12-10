using Assets.Utils.Formatting;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class Companies
    {
        public static void AttachToGroup(GameContext context, int parentId, int subsidiaryId)
        {
            // TODO only possible if independent!

            var parent = GetCompany(context, parentId);


            // we cannot attach company to product company
            if (!IsCompanyGroupLike(parent))
                return;

            var daughter = GetCompany(context, subsidiaryId);

            if (daughter.hasProduct)
            {
                AddFocusNiche(daughter.product.Niche, parent, context);
                var industry = Markets.GetIndustry(daughter.product.Niche, context);

                AddFocusIndustry(industry, parent);
            }


            //Debug.Log("Attach " + daughter.company.Name + " to " + parent.company.Name);

            var shareholders = new Dictionary<int, BlockOfShares>
            {
                {
                    parent.shareholder.Id,
                    new BlockOfShares
                    {
                        amount = 100,
                        InvestorType = InvestorType.Strategic,
                        shareholderLoyalty = 100
                    }
                }
            };

            if (daughter.hasShareholders)
                daughter.ReplaceShareholders(shareholders);
            else
                daughter.AddShareholders(shareholders);

            daughter.isIndependentCompany = false;
        }

        public static int CreateProductAndAttachItToGroup(GameContext gameContext, NicheType nicheType, GameEntity group)
        {
            //var startCapital = NicheUtils.GetStartCapital(nicheType, gameContext) / 2;

            //if (!IsEnoughResources(group, startCapital))
            //    return;

            //SpendResources(group, startCapital);



            string name = group.company.Name + " " + EnumUtils.GetFormattedNicheName(nicheType);

            if (GetCompanyByName(gameContext, name) != null)
                name += " " + Markets.GetCompetitorsAmount(nicheType, gameContext);

            var c = GenerateProductCompany(gameContext, name, nicheType);

            AttachToGroup(gameContext, group.company.Id, c.company.Id);

            return c.company.Id;
        }

    }
}
