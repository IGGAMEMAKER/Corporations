using System;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    static partial class Economy
    {
        public static long GetProductCompanyIncome(GameEntity e, GameContext context)
        {
            if (e.isDumping)
                return 0;

            float income = GetIncomePerUser(context, e) * Marketing.GetClients(e);

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

        public static float GetIncomePerUser(GameContext gameContext, GameEntity c)
        {
            //float price = Markets.GetBaseProductPrice(c, gameContext);
            float price = GetBaseIncomeByMonetisationType(gameContext, c);

            var improvements = 100f + Products.GetMonetisationFeaturesBenefit(c);

            return price * improvements / 100f;
        }


        // not used
        public static float GetBaseIncomeByMonetisationType(GameContext gameContext, GameEntity c)
        {
            var niche = Markets.Get(gameContext, c.product.Niche);

            var pricingType = niche.nicheBaseProfile.Profile.MonetisationType;

            var baseValue = GetBaseIncomeByMonetisationType(pricingType);
            
            return baseValue;
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
    }
}
