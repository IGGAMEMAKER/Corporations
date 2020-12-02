using System;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        //public static int GetTeamSize(GameEntity e)
        public static int GetTotalEmployees(GameEntity e)
        {
            return e.team.Teams.Sum(t => GetMaxTeamSize(t));
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

        public static GameEntity GetWorkerByRole(GameEntity company, WorkerRole role, TeamInfo teamInfo, GameContext gameContext)
        {
            var managers = teamInfo.Managers.Select(humanId => Humans.Get(gameContext, humanId));

            var workersWithRole = managers.Where(h => h.worker.WorkerRole == role);

            return workersWithRole.Count() > 0 ? workersWithRole.First() : null;
        }

        public static bool HasRole(GameEntity company, WorkerRole role, TeamInfo teamInfo, GameContext gameContext)
        {
            var managers = teamInfo.Managers.Select(humanId => Humans.Get(gameContext, humanId));

            var workersWithRole = managers.Where(h => h.worker.WorkerRole == role);

            return workersWithRole.Count() > 0;
        }

        public static bool IsNeedsToHireRole(GameEntity company, WorkerRole role, TeamInfo teamInfo, GameContext gameContext)
        {
            var roles = GetRolesForTeam(teamInfo.TeamType);

            if (!roles.Contains(role))
                return false;

            return !HasRole(company, role, teamInfo, gameContext);
        }

        public static bool IsCanAddMoreTeams(GameEntity company, GameContext gameContext)
        {
            return true;
            var culture = Companies.GetOwnCulture(company);

            return culture[CorporatePolicy.DoOrDelegate] > 1;
                //return true;
            
            //return company.team.Teams.Count < GetMaxTeamsAmount(company, gameContext);
        }

        public static int GetMaxTeamsAmount(GameEntity company, GameContext gameContext)
        {
            var culture = Companies.GetActualCorporateCulture(company);

            if (company.isFlagship)
            {
                var managingCompany = Companies.GetManagingCompanyOf(company, gameContext);

                culture = managingCompany.corporateCulture.Culture;
            }

            return culture[CorporatePolicy.DoOrDelegate];
        }
    }
}
