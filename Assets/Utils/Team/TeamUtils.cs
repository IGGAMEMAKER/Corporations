using Assets.Classes;

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

namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        private static void ReplaceTeam(GameEntity company, TeamComponent t)
        {
            company.ReplaceTeam(t.Morale, t.Workers, t.TeamStatus);
        }

        internal static void ToggleCrunching(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

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

        public static TeamResource GetTeamPromotionCost(GameEntity company)
        {
            var managerPoints = 100;

            switch (company.team.TeamStatus)
            {
                case TeamStatus.Solo: managerPoints = 100; break;
                case TeamStatus.Pair: managerPoints = 225; break;
                case TeamStatus.SmallTeam: managerPoints = 350; break;
                case TeamStatus.Department: managerPoints = 700; break;
            }

            return new TeamResource(0, managerPoints, 0, 0, 0);
        }

        internal static void Promote(GameEntity company, TeamStatus newTeamStatus)
        {
            var team = company.team;

            company.ReplaceTeam(team.Morale, team.Workers, newTeamStatus);
        }

        internal static TeamStatus GetNextTeamSize(TeamStatus teamStatus)
        {
            switch (teamStatus)
            {
                case TeamStatus.Solo:
                    return TeamStatus.Pair;

                case TeamStatus.Pair:
                    return TeamStatus.SmallTeam;

                case TeamStatus.SmallTeam:
                    return TeamStatus.Department;

                case TeamStatus.Department:
                    return TeamStatus.BigTeam;

                default: return TeamStatus.BigTeam;
            }
        }

        internal static void Promote(GameEntity company)
        {
            var team = company.team;

            TeamStatus newTeamStatus = GetNextTeamSize(team.TeamStatus);

            Promote(company, newTeamStatus);
        }
    }
}
