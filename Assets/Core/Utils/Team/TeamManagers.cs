using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static void SetRole(GameEntity company, int humanId, WorkerRole workerRole, GameContext gameContext)
        {
            var managers = company.team.Managers;

            managers[humanId] = workerRole;

            company.ReplaceTeam(company.team.Morale, company.team.Organisation, managers, company.team.Workers, company.team.Teams, company.team.Salaries);

            Humans.SetRole(gameContext, humanId, workerRole);
        }

        public static WorkerRole GetMainManagerRole(TeamInfo teamInfo)
        {
            WorkerRole managerTitle;

            if (teamInfo.isCoreTeam)
            {
                return WorkerRole.CEO;
            }

            switch (teamInfo.TeamType)
            {
                case TeamType.ServersideTeam:
                case TeamType.DevelopmentTeam:
                    managerTitle = WorkerRole.TeamLead;
                    break;

                case TeamType.SupportTeam:
                case TeamType.MarketingTeam:
                    managerTitle = WorkerRole.MarketingLead;
                    break;

                default:
                    managerTitle = WorkerRole.ProjectManager;
                    break;
            }

            return managerTitle;
        }

        public static bool HasMainManagerInTeam(TeamInfo teamInfo)
        {
            return HasRole(GetMainManagerRole(teamInfo), teamInfo);
        }

        public static bool NeedsMainManagerInTeam(TeamInfo team)
        {
            return !HasMainManagerInTeam(team);
        }

        public static IEnumerable<int> GetProperCandidatesFrom(Dictionary<int, WorkerRole> managers, TeamInfo team, bool hasLeader, WorkerRole leaderRole)
        {
            return managers
                    .Where(RoleSuitsTeam(team))

                    // 
                    .Where(m => hasLeader || m.Value == leaderRole)
                    .Select(p => p.Key)
                ;
        }
        public static List<int> GetCandidatesForTeam(GameEntity company, TeamInfo team, GameContext Q,
            bool includeCompetingCompaniesWorkers = false)
        {
            var managerIds = new List<int>();

            bool hasLeader = HasMainManagerInTeam(team);
            var leaderRole = GetMainManagerRole(team);

            managerIds.AddRange(
                GetProperCandidatesFrom(company.employee.Managers, team, hasLeader, leaderRole)
            );

            if (includeCompetingCompaniesWorkers)
            {
                foreach (var c in Companies.GetCompetitorsOf(company, Q, false))
                {
                    managerIds.AddRange(
                        GetProperCandidatesFrom(c.team.Managers, team, hasLeader, leaderRole)
                    );
                }
            }


            return managerIds;
        }
        
        public static List<int> GetCandidatesForRole(GameEntity company, GameContext gameContext, WorkerRole WorkerRole)
        {
            var managerIds = new List<int>();

            managerIds.AddRange(company.employee.Managers.Where(p => p.Value == WorkerRole).Select(p => p.Key));

            var competitors = Companies.GetCompetitorsOf(company, gameContext, false);

            foreach (var c in competitors)
            {
                var workers = c.team.Managers.Where(p => p.Value == WorkerRole).Select(p => p.Key);
                managerIds.AddRange(workers);
            }

            return managerIds;
        }

        public static void SetManagerTask(GameEntity company, int teamId, int taskId, ManagerTask managerTask)
        {
            var tasks = company.team.Teams[teamId].ManagerTasks;

            if (tasks.Count < taskId)
            {
                tasks.Add(managerTask);
            }
            else
            {
                tasks[taskId] = managerTask;
            }

            //company.team.Teams[teamId].ManagerTasks[taskId] = managerTask;
        }
    }
}
