using UnityEngine;

namespace Assets.Utils
{
    public static partial class Products
    {
        public static int GetBaseIterationTime(GameEntity niche) => GetBaseIterationTime(niche.nicheBaseProfile.Profile.NicheSpeed);
        private static int GetBaseIterationTime(NicheSpeed nicheChangeSpeed)
        {
            return 30;

            var modifier = 3;
            switch (nicheChangeSpeed)
            {
                case NicheSpeed.Quarter:   return 90 / modifier;

                case NicheSpeed.HalfYear:  return 180 / modifier;
                case NicheSpeed.Year:      return 360 / modifier;

                case NicheSpeed.ThreeYears: return 360 * 3 / modifier;

                default: return 0;
            }
        }

        public static int GetProductUpgradeIterationTime(GameContext gameContext, GameEntity company)
        {
            var niche = Markets.GetNiche(gameContext, company);
            var baseIterationTime = GetBaseIterationTime(niche);

            var innovationSpeed = 150 * Random.Range(10, 13) / 10;

            var innovationPenalty   = IsWillInnovate(company, gameContext) ? innovationSpeed : 0;
            var companyLimitPenalty = Companies.GetCompanyLimitPenalty(company, gameContext);

            var modifiers = 100 + innovationPenalty + companyLimitPenalty;


            var time = (int) (baseIterationTime * modifiers / 100f);

            return time;
        }

        public static int GetTimeToMarketFromScratch(GameEntity niche)
        {
            var demand = GetMarketDemand(niche);
            var iterationTime = GetBaseIterationTime(niche);

            return demand * iterationTime / 30;
        }
    }
}
