using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Teams
    {
        // TODO duplicates?
        public static GameEntity GetWorkerInRole(TeamInfo team, WorkerRole workerRole, GameContext gameContext)
        {
            var managers = team.Managers.Select(humanId => Humans.Get(gameContext, humanId)).Where(worker => worker.worker.WorkerRole == workerRole).ToList();

            if (managers.Any())
            {
                return managers.First();
            }

            return null;
        }

        // TODO duplicates? This seems more effective
        public static GameEntity GetWorkerByRole(WorkerRole role, TeamInfo teamInfo, GameContext gameContext)
        {
            foreach (var pair in teamInfo.Roles)
            {
                if (pair.Value == role)
                    return Humans.Get(gameContext, pair.Key);
            }

            return null;
        }

        public static bool HasRole(WorkerRole role, TeamInfo teamInfo)
        {
            return teamInfo.Roles.ContainsValue(role);
        }

        public static bool IsNeedsToHireRole(WorkerRole role, TeamInfo teamInfo)
        {
            var roles = GetRolesForTeam(teamInfo);

            if (!roles.Contains(role))
                return false;

            return !HasRole(role, teamInfo);
        }


        // Missing roles
        public static IEnumerable<WorkerRole> GetMissingRolesFull(TeamInfo team)
        {
            var hasProgrammers = team.Roles.Values.Any(r => r == WorkerRole.Programmer);
            var hasMarketers = team.Roles.Values.Any(r => r == WorkerRole.Marketer);

            var hasLeader = HasMainManagerInTeam(team);

            // hire programmers / marketers first
            // then you can add leads and stuff

            if (hasProgrammers && hasMarketers)
            {
                return Teams.GetMissingRoles(team);
            }
            else
            {
                var list = new List<WorkerRole>() { WorkerRole.Marketer, WorkerRole.Programmer };
                var leadRole = GetMainManagerRole(team);

                if (!hasLeader)
                    return new List<WorkerRole>() { leadRole };

                list.Add(leadRole);
                return list;
            }
        }
        public static IEnumerable<WorkerRole> GetMissingRoles(TeamInfo t)
        {
            bool hasLeader = HasMainManagerInTeam(t);
            var leaderRole = GetMainManagerRole(t);
            
            var allRoles = GetRolesForTeam(t).Where(r => hasLeader || r == leaderRole);

            return allRoles;
        }
        

        public static IEnumerable<WorkerRole> GetRolesForTeam(TeamInfo team)
        {
            var roles = GetRolesForTeam(team.TeamType).ToList();

            var mainRole = GetMainManagerRole(team);

            if (team.isCoreTeam)
            {
                // CEO only
                roles.Remove(WorkerRole.ProjectManager);
            }
            else
            {
                // regular universal team
                if (IsUniversalTeam(team.TeamType))
                {
                    roles.Remove(WorkerRole.CEO);
                }
            }

            /*if (team.Rank == TeamRank.Solo || team.Rank == TeamRank.SmallTeam)
            {
                roles.RemoveAll(r => r != mainRole);
            }*/

            return roles;
        }
        




        public static Func<KeyValuePair<int, WorkerRole>, bool> RoleSuitsTeam(TeamInfo team) => pair => IsRoleSuitsTeam(pair.Value, team);
        
        public static bool IsRoleSuitsTeam(WorkerRole workerRole, TeamInfo team)
        {
            var roles = GetRolesForTeam(team);

            return roles.Contains(workerRole);
        }
        
        public static List<WorkerRole> GetGroupRoles() => new List<WorkerRole>
        {
            WorkerRole.CEO,
            WorkerRole.MarketingDirector,
            WorkerRole.TechDirector
        };

        public static List<WorkerRole> GetProductCompanyRoles() => new List<WorkerRole>
        {
            WorkerRole.CEO,
            WorkerRole.MarketingLead,
            WorkerRole.TeamLead,
            WorkerRole.ProjectManager,
            WorkerRole.ProductManager,

            WorkerRole.Programmer,
            WorkerRole.Marketer
        };

        public static List<WorkerRole> GetRolesTheoreticallyPossibleForThisCompanyType(GameEntity company)
        {
            var roles = new List<WorkerRole>();

            switch (company.company.CompanyType)
            {
                case CompanyType.Corporation:
                case CompanyType.Group:
                case CompanyType.Holding:
                    roles = GetGroupRoles();
                    break;

                case CompanyType.ProductCompany:
                    roles = GetProductCompanyRoles();
                    break;

                case CompanyType.FinancialGroup:
                    break;
            }

            return roles;
        }


        
        private static IEnumerable<WorkerRole> GetRolesForTeam(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.SupportTeam:
                case TeamType.MarketingTeam: return new[] { WorkerRole.MarketingLead };

                case TeamType.DevelopmentTeam: return new[] { WorkerRole.TeamLead, WorkerRole.ProductManager };
                case TeamType.ServersideTeam: return new[] { WorkerRole.TeamLead };

                default:
                    return new[]
                    {
                        WorkerRole.CEO,
                        WorkerRole.ProjectManager,
                        WorkerRole.TeamLead,
                        WorkerRole.MarketingLead,
                        WorkerRole.ProductManager,

                        WorkerRole.Programmer,
                        WorkerRole.Marketer,
                    };
            }
        }



        public static string GetRoleDescription(WorkerRole role, GameContext gameContext, bool isUnemployed, GameEntity company = null)
        {
            var description = "";
            bool employed = !isUnemployed;

            switch (role)
            {
                case WorkerRole.CEO:
                    description = $"Manages entire company";
                    break;

                case WorkerRole.TeamLead:
                    description = $"Increases team speed";
                    break;

                case WorkerRole.MarketingLead:
                    description = $"Makes marketing more efficient";
                    //if (employed)
                    //    description += $" by {RenderBonus(Teams.GetMarketingLeadBonus(company, gameContext))}%";
                    break;

                case WorkerRole.ProductManager:
                    description = $"Makes better features";
                    break;
                case WorkerRole.ProjectManager:
                    description = $"??Reduces amount of workers";
                    break;

                case WorkerRole.Programmer:
                    description = $"Gives 1 feature points";
                    break;

                case WorkerRole.Marketer:
                    description = $"Runs 1 marketing campaign at time";
                    break;

                case WorkerRole.MarketingDirector:
                case WorkerRole.TechDirector:
                case WorkerRole.Universal:
                default:
                    description = "";
                    break;
            }

            return description;
        }

        public static int GetWorkerOrder(WorkerRole role)
        {
            if (role == WorkerRole.CEO)
                return 150;

            if (role == WorkerRole.Universal)
                return 110;

            if (role == WorkerRole.TechDirector)
                return 90;

            if (role == WorkerRole.MarketingDirector)
                return 80;

            if (role == WorkerRole.TeamLead)
                return 70;

            if (role == WorkerRole.MarketingLead)
                return 60;

            if (role == WorkerRole.ProjectManager)
                return 50;

            if (role == WorkerRole.ProductManager)
                return 40;

            if (role == WorkerRole.Marketer)
                return 30;

            if (role == WorkerRole.Programmer)
                return 20;

            return 0;
        }
    }
}
