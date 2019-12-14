using Assets.Utils.Formatting;
using UnityEngine;

namespace Assets.Utils
{
    partial class Economy
    {
        // resulting costs
        internal static long GetProductCompanyMaintenance(GameEntity e, GameContext gameContext)
        {
            var development = GetDevelopmentCost(e, gameContext);
            var marketing = GetMarketingCost(e, gameContext);

            return development + marketing;
        }

        public static long GetMarketingCost(GameEntity e, GameContext gameContext) => GetMarketingCost(e, gameContext, GetMarketingFinancingCostMultiplier(e));
        public static long GetMarketingCost(GameEntity e, GameContext gameContext, float financing)
        {
            var gainedClients = MarketingUtils.GetAudienceGrowth(e, gameContext);
            var acquisitionCost = Markets.GetClientAcquisitionCost(e.product.Niche, gameContext);

            var culture = Companies.GetActualCorporateCulture(e, gameContext);
            var creation = culture[CorporatePolicy.CreateOrBuy];

            // up to 40%
            var discount = 100 - (creation - 1) * 10;
            var result = gainedClients * acquisitionCost * financing * discount / 100;

            return (long)result;
        }


        public static long GetDevelopmentCost(GameEntity e, GameContext gameContext)
        {
            var workers = GetAmountOfWorkers(e, gameContext);

            var salaries = workers * Constants.SALARIES_PROGRAMMER;

            var culture = Companies.GetActualCorporateCulture(e, gameContext);
            var mindset = culture[CorporatePolicy.WorkerMindset];

            var discount = 100 - (mindset - 1) * 10;
            return salaries * discount / 100;
        }

        public static int GetAmountOfWorkers(GameEntity e, GameContext gameContext)
        {
            var concept = Products.GetProductLevel(e);
            var niche = Markets.GetNiche(gameContext, e);
            var complexity = (int)niche.nicheBaseProfile.Profile.AppComplexity;

            return (int)Mathf.Pow(1 + complexity / 20f, concept);
        }
    }
}
