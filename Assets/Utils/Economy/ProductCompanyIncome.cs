using System;

namespace Assets.Utils
{
    static partial class CompanyEconomyUtils
    {
        internal static long GetProductCompanyIncome(GameEntity e, GameContext context)
        {
            float income = 0;

            var segmentId = e.productPositioning.Positioning;
            income += GetIncomeBySegment(context, e.company.Id, segmentId);

            return Convert.ToInt64(income);
        }

        internal static float GetIncomeBySegment(GameContext gameContext, int companyId, int segmentId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            long clients = c.marketing.clients;

            float price = GetSegmentPrice(gameContext, companyId, segmentId);

            return clients * price;
        }

        internal static float GetSegmentPrice(GameContext gameContext, int companyId, int segmentId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            var improvements = 1; // c.expertise.ExpertiseLevel;

            var segmentPriceModifier = NicheUtils.GetSegmentProductPriceModifier(gameContext, c.product.Niche, segmentId);

            return (100 + 5 * improvements) * GetProductPrice(c, gameContext) * segmentPriceModifier / 100;
        }

        public static float GetProductPrice(GameEntity e, GameContext context)
        {
            return GetBaseProductPrice(e, context);
        }

        public static float GetBaseProductPrice(GameEntity e, GameContext context)
        {
            return e.finance.basePrice;
        }
    }
}
