namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        //public static void 
        public static void DisableTeamImprovement(GameEntity company, TeamUpgrade teamUpgrade)
        {
            company.teamImprovements.Upgrades.Remove(teamUpgrade);
        }
        public static void PickTeamImprovement(GameEntity company, TeamUpgrade teamUpgrade)
        {
            if (HasEnoughAvailableWorkersForImprovement(company, teamUpgrade))
                company.teamImprovements.Upgrades[teamUpgrade] = 1;
        }

        public static void ToggleTeamImprovement(GameEntity company, TeamUpgrade teamUpgrade)
        {
            if (IsUpgradePicked(company, teamUpgrade))
                DisableTeamImprovement(company, teamUpgrade);
            else
                PickTeamImprovement(company, teamUpgrade);
        }

        public static bool HasEnoughAvailableWorkersForImprovement (GameEntity company, TeamUpgrade teamUpgrade)
        {
            var requiredWorkers = GetRequiredAmountOfWorkersForTeamImprovement(teamUpgrade);
            var available = GetAvailableWorkers(company);

            return available >= requiredWorkers;
        }

        public static int GetRequiredAmountOfWorkersForTeamImprovement(TeamUpgrade teamUpgrade)
        {
            switch (teamUpgrade)
            {
                case TeamUpgrade.DevelopmentPrototype: return 1;
                case TeamUpgrade.DevelopmentPolishedApp: return 4;
                case TeamUpgrade.DevelopmentCrossplatform: return 30;

                case TeamUpgrade.MarketingBase: return 1;
                case TeamUpgrade.MarketingAggressive: return 7;
                case TeamUpgrade.AllPlatformMarketing: return 5;
                case TeamUpgrade.ClientSupport: return 1;
                case TeamUpgrade.ImprovedClientSupport: return 5;

                default: return 1000;
            }
        }

        public static bool IsUpgradePicked(GameEntity company, TeamUpgrade teamUpgrade)
        {
            var upgs = company.teamImprovements.Upgrades;

            return upgs.ContainsKey(teamUpgrade);
        }

        public static int GetAvailableWorkers(GameEntity company)
        {
            var max = GetTeamMaxSize(company);
            var active = GetActiveWorkers(company);

            return max - active;
        }

        public static int GetActiveWorkers(GameEntity company)
        {
            var upgrades = company.teamImprovements.Upgrades;

            var workers = 0;
            foreach (var imp in upgrades)
                workers += IsUpgradePicked(company, imp.Key) ? GetRequiredAmountOfWorkersForTeamImprovement(imp.Key) : 0;

            return workers;
        }
    }
}
