using UnityEngine;

namespace Assets.Core
{
    partial class Economy
    {
        // resulting costs
        public static Bonus<long> GetProductCompanyMaintenance(GameEntity e, GameContext gameContext, bool isBonus)
        {
            var bonus = new Bonus<long>("Maintenance");

            bonus
                .AppendAndHideIfZero("Workers", GetWorkersCost(e, gameContext) * C.PERIOD / 30)
                .AppendAndHideIfZero("Managers", GetManagersCost(e, gameContext) * C.PERIOD / 30);

            var upgrades = e.productUpgrades.upgrades;

            foreach (var u in upgrades)
            {
                long cost = 0;

                if (u.Value)
                    cost = Products.GetUpgradeCost(e, gameContext, u.Key) * C.PERIOD / 30;

                bonus.AppendAndHideIfZero(u.Key.ToString(), cost);
            }

            return bonus;
        }

        public static long GetProductCompanyMaintenance(GameEntity e, GameContext gameContext)
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
            var workers = Teams.GetTeamSize(e);

            return workers * C.SALARIES_PROGRAMMER; // * GetCultureTeamDiscount(e, gameContext) / 100;
        }

        public static long GetManagersCost(GameEntity e, GameContext gameContext)
        {
            var managers = e.team.Managers.Count;

            return managers * C.SALARIES_DIRECTOR;
        }
    }
}
