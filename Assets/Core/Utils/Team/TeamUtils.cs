using Assets.Core;
// TODO REMOVE THIS FILE

namespace Assets.Core
{
    public static partial class Teams
    {
        private static void ReplaceTeam(GameEntity company, TeamComponent t)
        {
            company.ReplaceTeam(t.Morale, t.Organisation, t.Managers, t.Workers, t.TeamStatus);
        }

        public static void ToggleCrunching(GameContext context, int companyId)
        {
            var c = Companies.Get(context, companyId);

            c.isCrunching = !c.isCrunching;
        }

        public static int GetTeamSizePerformanceModifier(GameEntity company)
        {
            var teamSizeModifier = 100;

            switch (company.team.TeamStatus)
            {
                case TeamStatus.Solo: teamSizeModifier = 50; break;
                case TeamStatus.Pair: teamSizeModifier = 85; break;
                case TeamStatus.SmallTeam: teamSizeModifier = 100; break;
                case TeamStatus.BigTeam: teamSizeModifier = 95; break;
                case TeamStatus.Department: teamSizeModifier = 80; break;
            }

            return teamSizeModifier;
        }

        public static void SetRole(GameEntity company, int humanId, WorkerRole workerRole, GameContext gameContext)
        {
            var managers = company.team.Managers;

            managers[humanId] = workerRole;

            company.ReplaceTeam(company.team.Morale, company.team.Organisation, managers, company.team.Workers, company.team.TeamStatus);

            Humans.SetRole(gameContext, humanId, workerRole);
        }
    }
}
