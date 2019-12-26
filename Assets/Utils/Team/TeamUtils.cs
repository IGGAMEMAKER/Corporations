using Assets.Core;
// TODO REMOVE THIS FILE
public enum WorkerRole {
    // base
    Programmer,
    // base
    Manager,
    // base
    Marketer,

    ProductManager,
    ProjectManager,

    TeamLead,
    MarketingLead,

    TechDirector,
    MarketingDirector,

    // base
    Business,


    Universal,
}

namespace Assets.Core
{
    public static partial class TeamUtils
    {
        private static void ReplaceTeam(GameEntity company, TeamComponent t)
        {
            company.ReplaceTeam(t.Morale, t.Workers, t.TeamStatus);
        }

        internal static void ToggleCrunching(GameContext context, int companyId)
        {
            var c = Companies.GetCompany(context, companyId);

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

        internal static int GetPerformance(GameContext gameContext, GameEntity company)
        {
            var teamSizeModifier = GetTeamSizePerformanceModifier(company);

            int crunchingModifier = company.isCrunching ? 40 : 0;

            return teamSizeModifier + crunchingModifier;
        }

        public static void SetRole(GameEntity company, int humanId, WorkerRole workerRole, GameContext gameContext)
        {
            var workers = company.team.Workers;

            workers[humanId] = workerRole;

            company.ReplaceTeam(company.team.Morale, workers, company.team.TeamStatus);

            HumanUtils.SetRole(gameContext, humanId, workerRole);
        }
    }
}
