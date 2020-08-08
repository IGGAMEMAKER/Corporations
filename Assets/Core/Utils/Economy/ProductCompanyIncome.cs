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

            long result = 0;

            try
            {
                foreach (var clientsList in e.marketing.ClientList)
                {
                    var segmentId = clientsList.Key;
                    var clients = clientsList.Value;

                    float income = GetIncomePerUser(context, e, segmentId) * clients;
                    result += Convert.ToInt64(income);
                }
            }
            catch
            {
                Debug.LogWarning("GetProductCompanyIncome " + Enums.GetFormattedNicheName(e.product.Niche) + " error " + e.company.Name);
            }

            return result * C.PERIOD / 30;
        }

        public static float GetMonetisationBonuses(GameContext gameContext, GameEntity c, int segmentId)
        {
            var audienceInfo = Marketing.GetAudienceInfos()[segmentId];

            var audienceBonus = audienceInfo.Bonuses.Where(b => b.isMonetisationFeature).Sum(b => b.Max);
            var improvements = 100f + Products.GetMonetisationFeaturesBenefit(c) + audienceBonus;

            return improvements;
        }
        public static float GetIncomePerUser(GameContext gameContext, GameEntity c, int segmentId)
        {
            //float price = Markets.GetBaseProductPrice(c, gameContext);
            float price = GetBaseIncomeByMonetisationType(gameContext, c);

            var bonuses = GetMonetisationBonuses(gameContext, c, segmentId);

            var income = price * bonuses / 100f;

            if (income < 0)
                return 0;

            return income;
        }


        // not used
        public static float GetBaseIncomeByMonetisationType(GameContext gameContext, GameEntity c) => GetBaseIncomeByMonetisationType(Markets.Get(gameContext, c.product.Niche));
        public static float GetBaseIncomeByMonetisationType(GameContext gameContext, NicheType c) => GetBaseIncomeByMonetisationType(Markets.Get(gameContext, c));
        public static float GetBaseIncomeByMonetisationType(GameEntity niche)
        {
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
