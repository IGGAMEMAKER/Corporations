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
        
        // ------------------- Management cost
        
        public static Bonus<float> GetManagerPointChange(GameEntity company, GameContext gameContext)
        {
            var teams = company.team.Teams;
        
            var bonus = new Bonus<float>("Point gain");

            bool teamsOnly = teams.Count > 3;

            foreach (var team in teams)
            {
                var b = GetTeamManagementBonus(team, company, gameContext);

                if (teamsOnly)
                {
                    bonus.Append(team.Name, b.Sum());
                }
                else
                {
                    bonus.Append(b);
                }
            }

            return bonus;
        }

        public static float GetTeamManagementGain(TeamInfo team, GameEntity company, GameContext Q)
        {
            var role = GetMainManagerRole(team);
            var manager = GetWorkerByRole(role, team, Q);
            var coreTeamBonus = team.isCoreTeam ? 3 : 1;

            var rating = GetEffectiveManagerRating(Q, company, manager);
        
            return coreTeamBonus * rating / 100f;
        }

        public static Bonus<float> GetTeamManagementBonus(TeamInfo team, GameEntity company, GameContext Q)
        {
            var bonus = new Bonus<float>("points");
        
            var role = GetMainManagerRole(team);

            var gain = GetTeamManagementGain(team, company, Q);
            var maintenance = GetManagementCostOfTeam(team);
        
            bonus.AppendAndHideIfZero(Humans.GetFormattedRole(role) + $" in " + team.Name, gain);
            bonus.Append("Maintenance of " + team.Name, -maintenance);

            return bonus;
        }
        
                public static int GetManagementCostOfTeam(TeamInfo team)
        {
            switch (team.Rank)
            {
                case TeamRank.Solo: return C.MANAGEMENT_COST_SOLO;
                case TeamRank.SmallTeam: return C.MANAGEMENT_COST_SMALL_TEAM;
                case TeamRank.BigTeam: return C.MANAGEMENT_COST_BIG_TEAM;
                case TeamRank.Department: return C.MANAGEMENT_COST_DEPARTMENT;

                default: return 0;
            }
        }

        public static int GetManagementCapacity(GameEntity company, GameContext gameContext)
        {
            var delegation = Companies.GetPolicyValue(company, CorporatePolicy.DoOrDelegate);
            var flatiness = Mathf.Clamp(Companies.GetPolicyValue(company, CorporatePolicy.DecisionsManagerOrTeam) / 2, 1, 5);

            int solo = C.MANAGEMENT_COST_SOLO; // 1
            int team = C.MANAGEMENT_COST_SMALL_TEAM; // 2;
            int bigTeam = C.MANAGEMENT_COST_BIG_TEAM; // 3;
            int department = C.MANAGEMENT_COST_DEPARTMENT; // 4;

            int company1 = C.MANAGEMENT_COST_COMPANY; // 100;

            int group = C.MANAGEMENT_COST_GROUP; // 1000;
            int corporation = C.MANAGEMENT_COST_CORPORATION; // 10_000;

            // 1...10

            switch (delegation)
            {
                case 1: return solo;
                case 2: return team * flatiness;
                case 3: return bigTeam * flatiness;
                case 4: return department * flatiness;

                case 5: return company1 * flatiness;
                case 6: return company1 * 2 * flatiness;
                case 7: return company1 * 4 * flatiness;

                case 8: return group * flatiness;
                case 9: return corporation * flatiness;

                default: return 1;
            }
        }

        public static int GetManagementEfficiency(GameEntity company, GameContext gameContext)
        {
            var capacity = GetManagementCapacity(company, gameContext);
            var cost = GetManagementCostOfCompany(company, gameContext, true);

            return Mathf.Clamp(capacity * 100 / cost, 1, 100);
        }

        public static int GetManagementCostOfCompany(GameEntity company, GameContext gameContext, bool recursive)
        {
            int value = 1;
            var teams = company.team.Teams.Sum(GetManagementCostOfTeam);

            if (recursive && Companies.IsGroup(company))
            {
                foreach (var h in Investments.GetOwnings(company, gameContext))
                {
                    value += GetManagementCostOfCompany(h, gameContext, recursive);
                }
            }

            return value + teams;
        }
    }
}
