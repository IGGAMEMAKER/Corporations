using Assets.Core;
// TODO REMOVE THIS FILE

namespace Assets.Core
{
    public static partial class Teams
    {
        private static void ReplaceTeam(GameEntity company, TeamComponent t)
        {
            company.ReplaceTeam(t.Morale, t.Organisation, t.Managers, t.Workers, t.Teams);
        }

        public static void ToggleCrunching(GameContext context, int companyId)
        {
            var c = Companies.Get(context, companyId);

            c.isCrunching = !c.isCrunching;
        }

        public static void SetRole(GameEntity company, int humanId, WorkerRole workerRole, GameContext gameContext)
        {
            var managers = company.team.Managers;

            managers[humanId] = workerRole;

            company.ReplaceTeam(company.team.Morale, company.team.Organisation, managers, company.team.Workers, company.team.Teams);

            Humans.SetRole(gameContext, humanId, workerRole);
        }

        public static void AddTeam(GameEntity company, TeamType teamType)
        {
            if (company.team.Teams.ContainsKey(teamType))
                company.team.Teams[teamType]++;
            else
                company.team.Teams[teamType] = 1;
        }

        public static void RemoveTeam(GameEntity company, TeamType teamType)
        {
            if (company.team.Teams.ContainsKey(teamType))
            {
                company.team.Teams[teamType]--;

                if (company.team.Teams[teamType] == 0)
                    company.team.Teams.Remove(teamType);
            }
        }

        public static int GetAmountOfWorkersByTeamType(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.MarketingTeam: return 3;
                case TeamType.DevelopmentTeam: return 3;
                case TeamType.SmallCrossfunctionalTeam: return 3;
                case TeamType.CrossfunctionalTeam: return 10;
                case TeamType.BigCrossfunctionalTeam: return 20;

                default: return 1000;
            }
        }

        public static int GetAmountOfTeams(GameEntity company, TeamType teamType)
        {
            return company.team.Teams.ContainsKey(teamType) ? company.team.Teams[teamType] : 0;
        }
    }
}
