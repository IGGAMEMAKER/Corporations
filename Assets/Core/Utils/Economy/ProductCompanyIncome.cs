using System;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    static partial class Economy
    {
        public static long GetProductCompanyIncome(GameEntity e, GameContext context)
        {
            long result = e.marketing.ClientList.Select(l => GetIncomePerSegment(context, e, l.Key)).Sum();

            return result * C.PERIOD / 30;
        }

        public static float GetMonetisationBonuses(GameContext gameContext, GameEntity c, int segmentId)
        {
            var audienceInfo = Marketing.GetAudienceInfos()[segmentId];

            var audienceBonus = audienceInfo.Bonuses.Where(b => b.isMonetisationFeature).Sum(b => b.Max);
            var improvements = 100f + Products.GetMonetisationFeaturesBenefit(c) + audienceBonus;

            return Mathf.Clamp(improvements, 0, 500);
        }

        public static long GetIncomePerSegment(GameContext gameContext, GameEntity company, int segmentId)
        {
            return Convert.ToInt64(company.marketing.ClientList[segmentId] * GetIncomePerUser(gameContext, company, segmentId));
        }

        public static float GetBaseIncomePerUser(GameContext gameContext, NicheType nicheType, int segmentId)
        {
            float price = GetBaseIncomeByMonetisationType(gameContext, nicheType);

            // apply segment bonuses
            // apply positioning bonuses??
            var bonuses = 100; // GetMonetisationBonuses(gameContext, c, segmentId);

            var income = price * bonuses / 100f;

            return income;
        }

        public static float GetIncomePerUser(GameContext gameContext, GameEntity c, int segmentId)
        {
            float price = GetBaseIncomeByMonetisationType(gameContext, c);

            var bonuses = GetMonetisationBonuses(gameContext, c, segmentId);

            var income = price * bonuses / 100f;

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
