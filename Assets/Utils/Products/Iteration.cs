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

        public static float GetConceptUpgradeEffeciency(GameContext gameContext, GameEntity company)
        {
            var teamSizeModifier = GetTeamSizeMultiplier(gameContext, company);

            // team lead
            var managerBonus = GetTeamLeadDevelopmentTimeDiscount(gameContext, company);

            return (100 - managerBonus) / teamSizeModifier / 100 / 100;
        }

        public static int GetTeamLeadDevelopmentTimeDiscount(GameContext gameContext, GameEntity company)
        {
            var teamLead = Teams.GetWorkerByRole(company, WorkerRole.TeamLead, gameContext);

            var managerBonus = 0;
            if (teamLead != null)
            {
                var rating = Humans.GetRating(gameContext, teamLead);
                var eff = Teams.GetWorkerEffeciency(teamLead, company);

                managerBonus = 50 * rating * eff / 100 / 100;
            }

            return managerBonus;
        }

        public static int GetUpgradeCost(GameEntity product, GameContext gameContext)
        {
            var baseCost = Products.GetBaseIterationTime(gameContext, product);

            bool willInnovate = Products.IsWillInnovate(product, gameContext);

            var innovationPenalty = willInnovate ? 250 : 100;

            return baseCost * innovationPenalty;
        }

        public static int GetTotalDevelopmentEffeciency(GameContext gameContext, GameEntity product)
        {
            var teamSizeModifier = Products.GetTeamEffeciency(gameContext, product);

            // team lead
            // 0...50
            var managerBonus = Products.GetTeamLeadDevelopmentTimeDiscount(gameContext, product);

            var speed = teamSizeModifier * (100 + managerBonus) / 100;

            return speed;
        }

        public static int GetConceptUpgradeTime(GameContext gameContext, GameEntity company)
        {
            var baseIterationTime   = GetBaseIterationTime(gameContext, company);

            var eff = GetConceptUpgradeEffeciency(gameContext, company);

            return (int) (baseIterationTime * eff);
        }

        public static int GetTimeToMarketFromScratch(GameEntity niche)
        {
            var demand = GetMarketDemand(niche);
            var iterationTime = GetBaseIterationTime(niche);

            return demand * iterationTime / 30;
        }
    }
}
