using UnityEngine;

namespace Assets.Core
{
    partial class Economy
    {
        // resulting costs
        internal static Bonus<long> GetProductCompanyMaintenance(GameEntity e, GameContext gameContext, bool isBonus)
        {
            var bonus = new Bonus<long>("Maintenance");

            bonus
                .AppendAndHideIfZero("Workers", GetWorkersCost(e, gameContext) * Balance.PERIOD / 30)
                .AppendAndHideIfZero("Managers", GetManagersCost(e, gameContext) * Balance.PERIOD / 30);

            var upgrades = e.productUpgrades.upgrades;

            foreach (var u in upgrades)
            {
                long cost = 0;

                if (u.Value)
                    cost = Products.GetUpgradeCost(e, gameContext, u.Key) * Balance.PERIOD / 30;

                bonus.AppendAndHideIfZero(u.Value.ToString(), cost);
            }

            return bonus;
        }

        internal static long GetProductCompanyMaintenance(GameEntity e, GameContext gameContext)
        {
            var bonus = GetProductCompanyMaintenance(e, gameContext, true);

            return bonus.Sum();
        }



        // team cost
        public static long GetTeamCost(GameEntity e, GameContext gameContext)
        {
            var managers = GetManagersCost(e, gameContext);
            var workers = GetWorkersCost(e, gameContext);

            return managers + workers;
        }

        public static long GetWorkersCost(GameEntity e, GameContext gameContext)
        {
            var workers = Teams.GetAmountOfWorkers(e, gameContext);

            return workers * Balance.SALARIES_PROGRAMMER; // * GetCultureTeamDiscount(e, gameContext) / 100;
        }

        public static long GetManagersCost(GameEntity e, GameContext gameContext)
        {
            var managers = e.team.Managers.Count;

            return managers * Balance.SALARIES_DIRECTOR;
        }

        public static int GetCultureTeamDiscount(GameEntity e, GameContext gameContext)
        {
            var culture = Companies.GetActualCorporateCulture(e, gameContext);
            var mindset = culture[CorporatePolicy.InnovationOrStability];

            // up to 40%
            var discount = 100 - (mindset - 1) * 5;

            return discount;
        }
    }
}
