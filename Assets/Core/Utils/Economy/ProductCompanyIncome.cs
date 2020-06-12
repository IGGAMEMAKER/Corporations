using System;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    static partial class Economy
    {
        public static long GetProductCompanyIncome(GameEntity e, GameContext context)
        {
            var segmentId = e.productPositioning.Positioning;
            float income = GetIncomeBySegment(context, e, segmentId);

            long result = 0;

            try
            {
                result = Convert.ToInt64(income);
            }
            catch
            {
                Debug.LogWarning("GetProductCompanyIncome " + Enums.GetFormattedNicheName(e.product.Niche) + " error " + e.company.Name);
            }

            return result * C.PERIOD / 30;
        }

        public static float GetIncomeBySegment(GameContext gameContext, GameEntity c, int segmentId)
        {
            if (c.isDumping)
                return 0;

            float unitIncome = GetIncomePerUser(gameContext, c, segmentId);

            long clients = Marketing.GetClients(c);

            return clients * unitIncome;
        }

        public static float GetIncomePerUser(GameContext gameContext, GameEntity c, int segmentId)
        {
            float price = GetBaseSegmentIncome(gameContext, c, segmentId);

            var improvements = GetMonetisationModifierFromImprovements(gameContext, c);


            //float monetisationModifier = GetBaseIncomeByMonetisationType(gameContext, c);

            var niche = Markets.Get(gameContext, c);
            if (niche.niche.NicheType == NicheType.ECom_Exchanging)
                Debug.Log($"Income per user is: {price.ToString("0.0")} + monetisation improvements: {improvements.ToString("0.0")}%");

            return price * (100f + improvements) / 100f;
        }

        public static float GetBaseSegmentIncome(GameContext gameContext, GameEntity c, int segmentId)
        {
            return Markets.GetSegmentProductPrice(gameContext, c.product.Niche, segmentId);
        }

        public static float GetBaseIncomeByMonetisationType(Monetisation monetisation)
        {
            switch (monetisation)
            {
                case Monetisation.Adverts:
                    return 0.3f;

                case Monetisation.Enterprise:
                    return 0.7f;

                case Monetisation.Paid:
                    return 0.7f;

                case Monetisation.Service:
                    return 0.3f;

                default:
                    return 0.15f;
            }
        }

        public static float GetBaseIncomeByMonetisationType(GameContext gameContext, GameEntity c)
        {
            var niche = Markets.Get(gameContext, c.product.Niche);

            var pricingType = niche.nicheBaseProfile.Profile.MonetisationType;

            var baseValue = GetBaseIncomeByMonetisationType(pricingType);
            
            return baseValue;
        }

        public static float GetMonetisationModifierFromImprovements(GameContext gameContext, GameEntity c)
        {
            var monetisationFeatures = Products.GetAvailableFeaturesForProduct(c).Where(f => f.FeatureBonus is FeatureBonusMonetisation);

            var improvements = 0f;
            foreach (var f in monetisationFeatures)
                improvements += Products.GetFeatureActualBenefit(c, f);

            return improvements;
        }
    }
}
