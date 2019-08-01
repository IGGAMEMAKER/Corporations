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
                case TeamStatus.Solo: teamSizeModifier = 125; break;
                case TeamStatus.Pair: teamSizeModifier = 100; break;
                case TeamStatus.SmallTeam: teamSizeModifier = 85; break;
                case TeamStatus.Department: teamSizeModifier = 75; break;
                case TeamStatus.BigTeam: teamSizeModifier = 65; break;
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

        internal static void Promote(GameEntity company)
        {
            var team = company.team;

            TeamStatus newTeamStatus;

            switch (team.TeamStatus)
            {
                case TeamStatus.Solo: newTeamStatus = TeamStatus.Pair; break;
                case TeamStatus.Pair: newTeamStatus = TeamStatus.SmallTeam; break;
                case TeamStatus.SmallTeam: newTeamStatus = TeamStatus.Department; break;
                case TeamStatus.Department: newTeamStatus = TeamStatus.BigTeam; break;

                default: newTeamStatus = TeamStatus.BigTeam; break;
            }

            company.ReplaceTeam(team.Morale, team.Workers, newTeamStatus);
        }

        internal static void Optimize(GameEntity company)
        {

        }
    }
}
