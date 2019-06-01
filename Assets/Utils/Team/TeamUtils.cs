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

        internal static int GetPerformance(GameContext gameContext, GameEntity company)
        {
            int crunchingModifier = company.isCrunching ? 40 : 0;

            return 100 + crunchingModifier;
        }




        public static void SetRole(GameEntity company, int humanId, WorkerRole workerRole)
        {
            var workers = company.team.Workers;

            workers[humanId] = workerRole;

            company.ReplaceTeam(company.team.Morale, workers, company.team.TeamStatus);
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
    }
}
