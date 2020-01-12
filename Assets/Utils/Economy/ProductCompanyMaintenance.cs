using UnityEngine;

namespace Assets.Core
{
    partial class Economy
    {
        // resulting costs
        internal static long GetProductCompanyMaintenance(GameEntity e, GameContext gameContext)
        {
            var development = GetDevelopmentCost(e, gameContext);
            var marketing = GetMarketingCost(e, gameContext);

            return (development + marketing) * Constants.PERIOD / 30;
        }

        public static long GetMarketingCost(GameEntity e, GameContext gameContext)
        {
            var cost = GetMarketingFinancingCostMultiplier(e, gameContext);

            var culture = Companies.GetActualCorporateCulture(e, gameContext);
            var creation = culture[CorporatePolicy.BuyOrCreate];

            // up to 40%
            var discount = 100 - (creation - 1) * 5;
            var result = cost * discount / 100;

            return (long)result;
        }


        public static long GetDevelopmentCost(GameEntity e, GameContext gameContext)
        {
            var workers = GetNecessaryAmountOfWorkers(e, gameContext);

            var cost = workers * Constants.SALARIES_PROGRAMMER;

            var culture = Companies.GetActualCorporateCulture(e, gameContext);
            var mindset = culture[CorporatePolicy.WorkerMindset];

            // up to 40%
            var discount = 100 - (mindset - 1) * 5;
            return cost * discount / 100;
        }



        public static int GetNecessaryAmountOfWorkers(GameEntity e, GameContext gameContext)
        {
            var concept = Products.GetProductLevel(e);
            var niche = Markets.GetNiche(gameContext, e);
            var complexity = (int)niche.nicheBaseProfile.Profile.AppComplexity;

            return (int)Mathf.Pow(1 + complexity / 20f, concept);
        }
    }
}
