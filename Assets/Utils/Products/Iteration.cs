using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        public static int GetBaseIterationTime(GameContext gameContext, GameEntity company) => GetBaseIterationTime(Markets.GetNiche(gameContext, company));
        public static int GetBaseIterationTime(GameEntity niche) => GetBaseIterationTime(niche.nicheBaseProfile.Profile.NicheSpeed);
        public static int GetBaseIterationTime(NicheSpeed nicheChangeSpeed)
        {
            return 21;

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


        public static int GetTeamEffeciency(GameContext gameContext, GameEntity product)
        {
            return (int) (100 * GetTeamSizeMultiplier(gameContext, product));
        }

        public static float GetTeamSizeMultiplier(GameContext gameContext, GameEntity company)
        {
            // +1 - CEO
            var have     = Teams.GetAmountOfWorkers(company, gameContext) + 1f;
            var required = Products.GetNecessaryAmountOfWorkers(company, gameContext) + 1f;

            if (have >= required)
                have = required;

            return have / required;
        }

        public static int GetConceptUpgradeTime(GameContext gameContext, GameEntity company)
        {
            var baseIterationTime   = GetBaseIterationTime(gameContext, company);
            var teamSizeModifier    = GetTeamSizeMultiplier(gameContext, company);

            // innovation penalty
            var innovationSpeed     = 150 * Random.Range(10, 13) / 10;
            bool willInnovate       = IsWillInnovate(company, gameContext);

            var innovationPenalty   = willInnovate ? innovationSpeed : 0;

            // project manager
            var projectManager      = Teams.GetWorkerByRole(company, WorkerRole.ProjectManager, gameContext);

            var projectManagerBonus = 0;
            if (projectManager != null)
            {
                var rating = Humans.GetRating(gameContext, projectManager);
                var eff = Teams.GetWorkerEffeciency(projectManager, company);

                projectManagerBonus = 50 * rating * eff / 100 / 100;
            }

            var modifiers           = 100 + innovationPenalty - projectManagerBonus;


            return (int) (baseIterationTime * modifiers / teamSizeModifier / 100);
        }

        public static int GetTimeToMarketFromScratch(GameEntity niche)
        {
            var demand = GetMarketDemand(niche);
            var iterationTime = GetBaseIterationTime(niche);

            return demand * iterationTime / 30;
        }
    }
}
