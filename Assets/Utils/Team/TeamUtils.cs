using System.Linq;

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
        private static void ReplaceTeam(GameEntity gameEntity, TeamComponent t)
        {
            gameEntity.ReplaceTeam(t.Morale, t.Workers, t.TeamStatus);
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

        public static int GetTeamMaxSize(GameEntity company)
        {
            switch (company.team.TeamStatus)
            {
                case TeamStatus.Solo:
                    return 1;

                case TeamStatus.Pair: return 2;

                case TeamStatus.SmallTeam: return 5;

                case TeamStatus.Department: return 20;

                default: return 11 + GetManagers(company) * 7;
            }
        }

        public static int CountSpecialists(GameEntity company, WorkerRole workerRole)
        {
            return company.team.Workers.Values.ToArray().Count(w => w == workerRole);
        }



        internal static int GetUniversals(GameEntity company)
        {
            return CountSpecialists(company, WorkerRole.Universal);
        }

        internal static int GetTopManagers(GameEntity company)
        {
            return 0;
        }

        public static int GetProgrammers(GameEntity company)
        {
            return CountSpecialists(company, WorkerRole.Programmer);
        }

        public static int GetManagers(GameEntity company)
        {
            return CountSpecialists(company, WorkerRole.Manager);
        }

        public static int GetMarketers(GameEntity company)
        {
            return CountSpecialists(company, WorkerRole.Marketer);
        }



        public static void SetRole(GameEntity company, int humanId, WorkerRole workerRole)
        {
            var workers = company.team.Workers;

            workers[humanId] = workerRole;

            company.ReplaceTeam(company.team.Morale, workers, company.team.TeamStatus);
        }

        public static int GetTeamSize(GameEntity company) {

            return company.team.Workers.Count();
        }

        public static bool IsWillNotOverextendTeam(GameEntity company)
        {
            return GetTeamSize(company) + 1 < GetTeamMaxSize(company);
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
