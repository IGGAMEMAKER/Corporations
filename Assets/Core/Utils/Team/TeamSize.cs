using System.Linq;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static int GetTotalEmployees(GameEntity e)
        {
            return e.team.Teams.Sum(GetMaxTeamSize);
        }

        public static int GetMaxTeamSize(TeamInfo team) => GetMaxTeamSize(team.Rank);
        public static int GetMaxTeamSize(TeamRank rank)
        {
            switch (rank)
            {
                case TeamRank.Solo: return 1;
                case TeamRank.SmallTeam: return 8;
                case TeamRank.BigTeam: return 20;
                case TeamRank.Department: return 100;

                default: return 100_000;
            }
        }

        public static GameEntity GetWorkerByRole(WorkerRole role, TeamInfo teamInfo, GameContext gameContext)
        {
            var managers = teamInfo.Managers.Select(humanId => Humans.Get(gameContext, humanId));

            var workersWithRole = managers.Where(h => h.worker.WorkerRole == role);

            return workersWithRole.Any() ? workersWithRole.First() : null;
        }

        public static bool HasRole(WorkerRole role, TeamInfo teamInfo)
        {
            return teamInfo.Roles.ContainsValue(role);
        }

        public static bool IsNeedsToHireRole(WorkerRole role, TeamInfo teamInfo, GameContext gameContext)
        {
            var roles = GetRolesForTeam(teamInfo.TeamType);

            if (!roles.Contains(role))
                return false;

            return !HasRole(role, teamInfo);
        }

        public static bool IsCanAddMoreTeams(GameEntity company, GameContext gameContext)
        {
            return true;
            var culture = Companies.GetOwnCulture(company);

            return culture[CorporatePolicy.DoOrDelegate] > 1;
                //return true;
            
            //return company.team.Teams.Count < GetMaxTeamsAmount(company, gameContext);
        }
    }
}
