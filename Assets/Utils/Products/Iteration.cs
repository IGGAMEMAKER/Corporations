using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        public static int GetBaseIterationTime(GameEntity niche) => GetBaseIterationTime(niche.nicheBaseProfile.Profile.NicheSpeed);
        private static int GetBaseIterationTime(NicheSpeed nicheChangeSpeed)
        {
            return 30;

            var modifier = 3; // 3
            switch (nicheChangeSpeed)
            {
                case NicheSpeed.Quarter:   return 90 / modifier;

                case NicheSpeed.HalfYear:  return 180 / modifier;
                case NicheSpeed.Year:      return 360 / modifier;

                case NicheSpeed.ThreeYears: return 360 * 3 / modifier;

                default: return 0;
            }
        }

        public static int GetTeamSizeIterationModifierMultipliedByHundred(GameContext gameContext, GameEntity company)
        {
            var have     = Teams.GetAmountOfWorkers(company, gameContext);
            var required = Products.GetNecessaryAmountOfWorkers(company, gameContext) + 1;

            if (have == 0)
                return 500;

            if (have >= required)
                return 0;

            return required * 100 / have;
        }

        public static int GetConceptUpgradeTime(GameContext gameContext, GameEntity company)
        {
            var niche = Markets.GetNiche(gameContext, company);
            var baseIterationTime = GetBaseIterationTime(niche);

            var innovationSpeed = 150 * Random.Range(10, 13) / 10;

            var crunchModifier = company.isCrunching ? -40 : 0;

            var teamSizeModifier    = GetTeamSizeIterationModifierMultipliedByHundred(gameContext, company);
            var innovationPenalty   = IsWillInnovate(company, gameContext) ? innovationSpeed : 0;
            var companyLimitPenalty = Companies.GetCompanyLimitPenalty(company, gameContext);

            var modifiers = 100 + innovationPenalty + companyLimitPenalty + crunchModifier;


            var time = (int) (baseIterationTime * modifiers * teamSizeModifier / 100 / 100f);

            return time;
        }

        public static int GetTimeToMarketFromScratch(GameEntity niche)
        {
            var demand = GetMarketDemand(niche);
            var iterationTime = GetBaseIterationTime(niche);

            return demand * iterationTime / 30;
        }


        public static string GetFormattedMonetisationType(GameEntity niche) => GetFormattedMonetisationType(niche.nicheBaseProfile.Profile.MonetisationType);
        public static string GetFormattedMonetisationType(Monetisation monetisation)
        {
            switch (monetisation)
            {
                case Monetisation.IrregularPaid: return "Irregular paid";
                default: return monetisation.ToString();
            }
        }
    }
}
