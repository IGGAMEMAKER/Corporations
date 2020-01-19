﻿using UnityEngine;

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
            return 0;
            var cost = GetMarketingFinancingCostMultiplier(e, gameContext);

            var culture = Companies.GetActualCorporateCulture(e, gameContext);
            var creation = culture[CorporatePolicy.BuyOrCreate];

            // up to 40%
            var discount = 100 - (creation - 1) * 5;
            var result = cost * discount / 100;

            return result;
        }


        public static long GetDevelopmentCost(GameEntity e, GameContext gameContext)
        {
            var workers = Teams.GetAmountOfWorkers(e, gameContext);
            var managers = e.team.Managers.Count;

            var cost = workers * Constants.SALARIES_PROGRAMMER + managers * Constants.SALARIES_DIRECTOR;

            var culture = Companies.GetActualCorporateCulture(e, gameContext);
            var mindset = culture[CorporatePolicy.WorkerMindset];

            // up to 40%
            var discount = 100 - (mindset - 1) * 5;
            return cost * discount / 100;
        }

        public static int GetNecessaryAmountOfProgrammers(GameEntity e, GameContext gameContext)
        {
            var concept = Products.GetProductLevel(e);
            var niche = Markets.GetNiche(gameContext, e);
            var complexity = (int)niche.nicheBaseProfile.Profile.AppComplexity;

            return (int)Mathf.Pow(1 + complexity / 20f, concept);
        }
        public static int GetNecessaryAmountOfMarketers(GameEntity e, GameContext gameContext)
        {
            var niche = Markets.GetNiche(gameContext, e);

            var clients = MarketingUtils.GetClients(e);

            return (int) Mathf.Pow(clients / 1000, 0.5f);
        }

        public static int GetNecessaryAmountOfWorkers(GameEntity e, GameContext gameContext)
        {
            return GetNecessaryAmountOfProgrammers(e, gameContext) + GetNecessaryAmountOfMarketers(e, gameContext);
        }
    }
}
