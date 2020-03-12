using UnityEngine;

namespace Assets.Core
{
    partial class Economy
    {
        // resulting costs
        internal static long GetProductCompanyMaintenance(GameEntity e, GameContext gameContext)
        {
            var development = GetTeamCost(e, gameContext);

            return development * Balance.PERIOD / 30;
        }

        public static long GetTeamCost(GameEntity e, GameContext gameContext)
        {
            var workers = Teams.GetAmountOfWorkers(e, gameContext);
            var managers = e.team.Managers.Count;

            var cost = workers * Balance.SALARIES_PROGRAMMER + managers * Balance.SALARIES_DIRECTOR;

            var culture = Companies.GetActualCorporateCulture(e, gameContext);
            var mindset = culture[CorporatePolicy.InnovationOrStability];

            // up to 40%
            var discount = 100 - (mindset - 1) * 5;
            return cost * discount / 100;
        }
    }
}
