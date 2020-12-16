using System;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    static partial class Economy
    {
        public static long GetProductIncome(GameEntity e)
        {
            long result = e.marketing.ClientList.Select(l => GetIncomePerSegment(e, l.Key)).Sum();

            return result * C.PERIOD / 30;
        }

        public static float GetMonetizationEfficiency(GameEntity c, int segmentId)
        {
            try
            {
                var audience = Marketing.GetAudienceInfos()[segmentId];

                var audienceBonus = audience.Bonuses.Where(b => b.isMonetisationFeature).Sum(b => b.Max);
                var improvements = Products.GetMonetisationFeaturesBenefit(c) * (100 + audienceBonus) / 100f;

                return Mathf.Clamp(improvements, 0, 500);
            }
            catch
            {
                // Debug.LogError("Tried to take monetization from segment " + segmentId + " in company " + c.company.Name);
            }

            return 0;
        }

        public static long GetIncomePerSegment(GameEntity company, int segmentId)
        {
            var clients = Marketing.GetUsers(company, segmentId);
            var incomePerUser = GetIncomePerUser(company, segmentId);

            return Convert.ToInt64(clients * incomePerUser);
        }

        //public static float GetBaseIncomePerUser(NicheType nicheType, int segmentId)
        //{
        //    float price = GetBaseIncomeByMonetisationType(,);

        //    // apply segment bonuses
        //    // apply positioning bonuses??
        //    var bonuses = 100; // GetMonetisationBonuses(gameContext, c, segmentId);

        //    var income = price * bonuses / 100f;

        //    return income;
        //}

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
