using Assets.Classes;

namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        public static void PickTeamImprovement(GameEntity company, TeamUpgrade teamUpgrade)
        {
            var activated = company.teamImprovements.Upgrades.ContainsKey(teamUpgrade);

            if (activated)
                company.teamImprovements.Upgrades.Remove(teamUpgrade);
            else
            {
                if (HasEnoughAvailableWorkersForImprovement(company, teamUpgrade))
                    company.teamImprovements.Upgrades[teamUpgrade] = 1;
            }
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
                case TeamUpgrade.Prototype: return 1;
                case TeamUpgrade.OnePlatformPaid: return 4;
                case TeamUpgrade.AllPlatforms: return 30;

                case TeamUpgrade.BaseMarketing: return 1;
                case TeamUpgrade.AggressiveMarketing: return 7;
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
            {
                workers += IsUpgradePicked(company, imp.Key) ? GetRequiredAmountOfWorkersForTeamImprovement(imp.Key) : 0;
            }

            return workers;
        }
    }
}
