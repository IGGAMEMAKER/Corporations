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
            var c = Companies.GetCompany(gameContext, companyId);

            if (c.isDumping)
                return 0;

            float price = GetSegmentPrice(gameContext, c, segmentId);

            var niche = Markets.GetNiche(gameContext, c.product.Niche);


            //var pricingType = niche.nicheBaseProfile.Profile.MonetisationType;

            //var isRegularPayingBusiness = pricingType == Monetisation.Adverts || pricingType == Monetisation.Service || pricingType == Monetisation.Enterprise;
            //var isIrregularPayingBusiness = pricingType == Monetisation.IrregularPaid;
            //var isPaidProduct = pricingType == Monetisation.Paid;

            //var monetisationTypeBonus = 1f;
            

            var isReleasedModifier = c.isRelease ? 1f : 0.5f;

            long clients = MarketingUtils.GetClients(c);
            return clients * price * isReleasedModifier;
        }

        internal static float GetSegmentPrice(GameContext gameContext, GameEntity c, int segmentId)
        {
            var segmentPriceModifier = Markets.GetSegmentProductPriceModifier(gameContext, c.product.Niche, segmentId);

            var basePrice = GetBaseProductPrice(c, gameContext);

            return basePrice * segmentPriceModifier;
        }

        public static float GetBaseProductPrice(GameEntity e, GameContext context)
        {
            var niche = Markets.GetNiche(context, e.product.Niche);

            return niche.nicheCosts.BaseIncome;
        }
    }
}
