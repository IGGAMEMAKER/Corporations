using Assets.Utils.Formatting;
using System;
using UnityEngine;

namespace Assets.Utils
{
    static partial class Economy
    {
        internal static long GetProductCompanyIncome(GameEntity e, GameContext context)
        {
            float income = 0;

            var segmentId = e.productPositioning.Positioning;
            income += GetIncomeBySegment(context, e.company.Id, segmentId);

            long result = 0;

            try
            {
                result = Convert.ToInt64(income);
            }
            catch
            {
                Debug.LogWarning("GetProductCompanyIncome " + EnumUtils.GetFormattedNicheName(e.product.Niche) + " error " + e.company.Name);
            }

            return result;
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

            var concept = 1 + Products.GetProductLevel(c) * 0.05f;

            return basePrice * segmentPriceModifier * concept;
        }

        public static float GetBaseProductPrice(GameEntity e, GameContext context)
        {
            var niche = Markets.GetNiche(context, e.product.Niche);

            return niche.nicheCosts.BaseIncome;
        }
    }
}
