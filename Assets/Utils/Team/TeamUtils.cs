using System;
using System.Linq;

public enum WorkerRole {
    Programmer,
    Manager,
    Marketer,

    ProductManager,
    ProjectManager,
    TechDirector,
    MarketingDirector,
    Business,
    Universal
}

namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        private static void ReplaceTeam(GameEntity gameEntity, TeamComponent t)
        {
            gameEntity.ReplaceTeam(t.Morale, t.Workers);
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
            return 228;
            //return company.team.Managers * 7;
        }

        static int CountSpecialists(GameEntity company, WorkerRole workerRole)
        {
            return company.team.Workers.Values.ToArray().Count(w => w == workerRole);
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

            company.ReplaceTeam(company.team.Morale, workers);
        }

        public static int GetTeamSize(GameEntity company) {

            return GetProgrammers(company) + GetManagers(company) + GetMarketers(company);
        }

        public static bool IsWillNotOverextendTeam(GameEntity company)
        {
            return GetTeamSize(company) + 1 < GetTeamMaxSize(company);
        }
    }
}
