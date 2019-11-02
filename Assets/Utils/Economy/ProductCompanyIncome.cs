using System;

namespace Assets.Utils
{
    static partial class EconomyUtils
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

            long clients = MarketingUtils.GetClients(c);

            var improvements = c.productImprovements.Improvements[ProductImprovement.Monetisation];
            float price = GetSegmentPrice(gameContext, c, segmentId);

            var niche = NicheUtils.GetNicheEntity(gameContext, c.product.Niche);

            var pricingType = niche.nicheBaseProfile.Profile.MonetisationType;

            var isRegularPayingBusiness = pricingType == Monetisation.Adverts || pricingType == Monetisation.Service || pricingType == Monetisation.Enterprise;
            var isIrregularPayingBusiness = pricingType == Monetisation.IrregularPaid;
            var isPaidProduct = pricingType == Monetisation.Paid;

            var financing = c.financing.Financing[Financing.Development];
            var financingModifier = 1;

            if (financing == 1)
                financingModifier = 10;

            if (financing == 2)
                financingModifier = 13;

            return clients * price * financingModifier / 10f;
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
