using Assets.Core;
using System;
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

        public static WorkerRole GetMainManagerForTheTeam(TeamInfo teamInfo)
        {
            WorkerRole managerTitle;

            bool isCoreTeam = teamInfo.ID == 0;

            if (isCoreTeam)
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
                    //managerTitle = WorkerRole.ProductManager;
                    break;
            }

            return managerTitle;
        }

        public static bool HasMainManagerInTeam(TeamInfo teamInfo, GameContext gameContext, GameEntity product)
        {
            WorkerRole managerTitle = GetMainManagerForTheTeam(teamInfo);

            var rating = GetEffectiveManagerRating(gameContext, product, managerTitle, teamInfo);

            return rating > 0;
        }

        public static bool NeedsMainManagerInTeam(TeamInfo team, GameContext gameContext, GameEntity product)
        {
            return !HasMainManagerInTeam(team, gameContext, product);
        }

        public static List<int> GetCandidatesForTeam(GameEntity company, TeamInfo team, GameContext Q)
        {
            var competitors = Companies.GetCompetitorsOf(company, Q, false);

            var managers = new List<GameEntity>();
            var managerIds = new List<int>();

            bool hasLeader = Teams.HasMainManagerInTeam(team, Q, company);
            var leaderRole = Teams.GetMainManagerForTheTeam(team);

            managerIds.AddRange(
                company.employee.Managers
                .Where(Teams.RoleSuitsTeam(company, team))

                // 
                .Where(m => hasLeader || m.Value == leaderRole)
                .Select(p => p.Key)
                );

            foreach (var c in competitors)
            {
                var workers = c.team.Managers
                    .Where(Teams.RoleSuitsTeam(company, team))

                    .Where(m => hasLeader || m.Value == leaderRole)
                    .Select(p => p.Key);
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
