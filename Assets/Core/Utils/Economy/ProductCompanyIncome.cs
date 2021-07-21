using System;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    static partial class Economy
    {
        public static long GetProductIncome(GameEntity e)
        {
            return GetIncomePerSegment(e) * C.PERIOD / 30;
        }

        public static float GetMonetizationEfficiency(GameEntity c, int segmentId)
        {
            var audience = Marketing.GetAudienceInfos()[segmentId];

            var audienceBonus = audience.Bonuses.Where(b => b.isMonetisationFeature).Sum(b => b.Max);
            var improvements = Products.GetMonetisationFeaturesBenefit(c) * (100 + audienceBonus) / 100f;

            return Mathf.Clamp(improvements, 0, 500);
        }

        public static long GetIncomePerSegment(GameEntity company)
        {
            int segmentId = 0;

            var clients = Marketing.GetUsers(company);
            var incomePerUser = GetIncomePerUser(company, segmentId);

            return Convert.ToInt64(clients * incomePerUser);
        }

        public static float GetIncomePerUser(GameEntity c, int segmentId)
        {
            float price = GetBaseIncomeByMonetizationType(c);

            var bonuses = GetMonetizationEfficiency(c, segmentId);
            // get positioning bonuses
            // get segment bonuses

            var income = price * bonuses / 100f;

            return income;
        }


        // where c = product or c = niche
        // both have nicheBaseProfile component
        public static float GetBaseIncomeByMonetizationType(GameEntity c) => GetBaseIncomeByMonetizationType(c.nicheBaseProfile.Profile.MonetisationType);
        public static float GetBaseIncomeByMonetizationType(Monetisation monetization)
        {
            switch (monetization)
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
