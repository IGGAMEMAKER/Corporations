using System;
using UnityEngine;

namespace Assets.Utils
{
    static partial class EconomyUtils
    {
        internal static long GetProductCompanyIncome(GameEntity e, GameContext context)
        {
            float income = 0;

            var productStageModifier = 0;

            if (TeamUtils.IsUpgradePicked(e, TeamUpgrade.DevelopmentPrototype))
                productStageModifier = 10;
            if (TeamUtils.IsUpgradePicked(e, TeamUpgrade.DevelopmentPolishedApp))
                productStageModifier = 3;
            if (TeamUtils.IsUpgradePicked(e, TeamUpgrade.DevelopmentCrossplatform))
                productStageModifier = 1;

            if (productStageModifier == 0)
                return 0;

            var segmentId = e.productPositioning.Positioning;
            income += GetIncomeBySegment(context, e.company.Id, segmentId);

            return Convert.ToInt64(income / productStageModifier);
        }

        internal static float GetIncomeBySegment(GameContext gameContext, int companyId, int segmentId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            long clients = MarketingUtils.GetClients(c);

            float price = GetSegmentPrice(gameContext, c, segmentId);

            return clients * price;
        }

        internal static float GetSegmentPrice(GameContext gameContext, GameEntity c, int segmentId)
        {
            var segmentPriceModifier = NicheUtils.GetSegmentProductPriceModifier(gameContext, c.product.Niche, segmentId);

            var basePrice = GetBaseProductPrice(c, gameContext);

            return basePrice * segmentPriceModifier;
        }

        public static float GetBaseProductPrice(GameEntity e, GameContext context)
        {
            var niche = NicheUtils.GetNicheEntity(context, e.product.Niche);

            return niche.nicheCosts.BasePrice;
        }
    }
}
