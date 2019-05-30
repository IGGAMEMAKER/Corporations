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

        public static int GetProgrammers(GameEntity company)
        {
            return 1;
        }

        public static int GetManagers(GameEntity company)
        {
            return 1;
        }

        public static int GetMarketers(GameEntity company)
        {
            return 1;
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
