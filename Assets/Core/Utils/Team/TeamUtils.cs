using Assets.Core;
using System;
using System.Linq;
using UnityEngine;
// TODO REMOVE THIS FILE

namespace Assets.Core
{
    public class SlotInfo
    {
        public int TeamId;
        public int SlotId;
    }

    public static partial class Teams
    {
        private static void ReplaceTeam(GameEntity company, TeamComponent t)
        {
            company.ReplaceTeam(t.Morale, t.Organisation, t.Managers, t.Workers, t.Teams);
        }

        public static void ToggleCrunching(GameContext context, int companyId)
        {
            var c = Companies.Get(context, companyId);

            c.isCrunching = !c.isCrunching;
        }

        public static void SetRole(GameEntity company, int humanId, WorkerRole workerRole, GameContext gameContext)
        {
            var managers = company.team.Managers;

            managers[humanId] = workerRole;

            company.ReplaceTeam(company.team.Morale, company.team.Organisation, managers, company.team.Workers, company.team.Teams);

            Humans.SetRole(gameContext, humanId, workerRole);
        }

        public static WorkerRole GetMainManagerForTheTeam(TeamInfo teamInfo)
        {
            WorkerRole managerTitle;

            bool isCoreTeam = teamInfo.ID == 0;

            if (isCoreTeam)
            {
                managerTitle = WorkerRole.CEO;
            }
            else
            {
                switch (teamInfo.TeamType)
                {
                    case TeamType.BigCrossfunctionalTeam:
                    case TeamType.CrossfunctionalTeam:
                    case TeamType.SmallCrossfunctionalTeam:
                        managerTitle = WorkerRole.ProjectManager;
                        break;

                    case TeamType.DevOpsTeam:
                    case TeamType.DevelopmentTeam:
                        managerTitle = WorkerRole.TeamLead;
                        break;

                    case TeamType.MarketingTeam:
                    case TeamType.SupportTeam:
                        managerTitle = WorkerRole.MarketingLead;
                        break;

                    default:
                        managerTitle = WorkerRole.ProductManager;
                        break;
                }
            }

            return managerTitle;
        }

        public static bool HasMainManagerInTeam(TeamInfo teamInfo, GameContext gameContext, GameEntity product)
        {
            WorkerRole managerTitle = GetMainManagerForTheTeam(teamInfo);

            var rating = GetEffectiveManagerRating(gameContext, product, managerTitle, teamInfo);

            return rating > 0;
        }

        public static Bonus<int> GetOrganisationChanges(TeamInfo teamInfo, GameEntity product, GameContext gameContext)
        {
            WorkerRole managerTitle = GetMainManagerForTheTeam(teamInfo);

            var rating = GetEffectiveManagerRating(gameContext, product, managerTitle, teamInfo);

            var managerFocus = teamInfo.ManagerTasks.Count(t => t == ManagerTask.Organisation);

            bool NoCeo = rating < 1;

            // count team size: small teams organise faster, big ones: slower

            return new Bonus<int>("Organisation change")
                .AppendAndHideIfZero("No " + managerTitle.ToString(), NoCeo ? -30 : 0)
                .AppendAndHideIfZero(managerTitle.ToString() + " efforts", rating)
                .AppendAndHideIfZero("Manager focus on Organisation", NoCeo ? 0 : managerFocus * 10)
                ;
        }

        public static bool IsUniversalTeam(TeamType teamType) => new TeamType[] { TeamType.BigCrossfunctionalTeam, TeamType.CoreTeam, TeamType.CrossfunctionalTeam, TeamType.SmallCrossfunctionalTeam }.Contains(teamType);

        public static string GetFormattedTeamType(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.BigCrossfunctionalTeam:   return "Big team";
                case TeamType.CrossfunctionalTeam:      return "Universal team";
                case TeamType.SmallCrossfunctionalTeam: return "Small team";

                case TeamType.CoreTeam:                 return "Core team";
                case TeamType.DevelopmentTeam:          return "Development team";
                case TeamType.MarketingTeam:            return "Marketing team";
                case TeamType.MergeAndAcquisitionTeam:  return "M&A team";
                case TeamType.SupportTeam:              return "Support team";

                case TeamType.DevOpsTeam:               return "Serverside team";

                default: return teamType.ToString();
            }
        }

        public static int AddTeam(GameEntity company, TeamType teamType)
        {
            var prefix = GetFormattedTeamType(teamType);

            bool isCoreTeam = company.team.Teams.Count == 0;

            company.team.Teams.Add(new TeamInfo {
                Name = isCoreTeam ? "Core team" : $"{prefix} #{company.team.Teams.Count}",
                TeamType = teamType, Tasks = new System.Collections.Generic.List<TeamTask>(),

                Offers = new System.Collections.Generic.Dictionary<int, JobOffer> { },
                Managers = new System.Collections.Generic.List<int>(),
                Roles = new System.Collections.Generic.Dictionary<int, WorkerRole>(),
                ManagerTasks = new System.Collections.Generic.List<ManagerTask> { ManagerTask.None, ManagerTask.None, ManagerTask.None },

                HiringProgress = 0, Workers = 0,
                Organisation = 0,
                ID = company.team.Teams.Count,

                TooManyLeaders = false
            });

            return company.team.Teams.Count - 1;
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

        public static void RemoveTeam(GameEntity company, GameContext gameContext, int teamId)
        {
            var tasks = company.team.Teams[teamId].Tasks;
            var count = tasks.Count;

            while (tasks.Count > 0)
            {
                Teams.RemoveTeamTask(company, gameContext, teamId, 0);
            }

            company.team.Teams.RemoveAt(teamId);

            int id = 0;
            foreach (var t in company.team.Teams)
            {
                t.ID = id++;
            }

            Debug.Log("Team removed!");
        }

        public static int GetAmountOfWorkersByTeamType(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.MarketingTeam: return 3;
                case TeamType.DevelopmentTeam: return 3;
                case TeamType.SmallCrossfunctionalTeam: return 3;
                case TeamType.CrossfunctionalTeam: return 10;
                case TeamType.BigCrossfunctionalTeam: return 20;

                default: return 1000;
            }
        }

        public static int GetAmountOfTeams(GameEntity company, TeamType teamType)
        {
            return company.team.Teams.FindAll(t => t.TeamType == teamType).Count;
        }

        public static int GetAmountOfPossibleChannelsByTeamType(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.BigCrossfunctionalTeam: return 3;
                case TeamType.CrossfunctionalTeam: return 2;
                case TeamType.SmallCrossfunctionalTeam: return 1;
                case TeamType.MarketingTeam: return 2;

                default: return 0;
            }
        }

        public static int GetAmountOfPossibleFeaturesByTeamType(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.BigCrossfunctionalTeam: return 3;
                case TeamType.CrossfunctionalTeam: return 2;
                case TeamType.SmallCrossfunctionalTeam: return 1;
                case TeamType.DevelopmentTeam: return 2;

                default: return 0;
            }
        }

        static bool IsUniversal(TeamType teamType) => new TeamType[] { TeamType.BigCrossfunctionalTeam, TeamType.CoreTeam, TeamType.CrossfunctionalTeam, TeamType.SmallCrossfunctionalTeam }.Contains(teamType);

        public static bool IsTaskSuitsTeam(TeamType teamType, TeamTask teamTask)
        {
            if (IsUniversal(teamType))
            {
                if (teamTask.IsHighloadTask && (teamTask as TeamTaskSupportFeature).SupportFeature.SupportBonus.Max > 1_000_000)
                    return false;

                return true;
            }

            if (teamTask.IsFeatureUpgrade)
                return teamType == TeamType.DevelopmentTeam;

            if (teamTask.IsMarketingTask)
                return teamType == TeamType.MarketingTeam;

            if (teamTask.IsSupportTask)
                return teamType == TeamType.SupportTeam;

            if (teamTask.IsHighloadTask)
                return teamType == TeamType.DevOpsTeam;

            return false;
        }

        public static int GetTeamIdForTask(GameEntity product, TeamTask teamTask)
        {
            // searching team for this task
            int teamId = 0;
            foreach (var t in product.team.Teams)
            {
                var hasFreeSlot = t.Tasks.Count < C.TASKS_PER_TEAM;
                var teamCanDoThisTask = IsTaskSuitsTeam(t.TeamType, teamTask);

                if (teamCanDoThisTask && hasFreeSlot)
                    return teamId;

                teamId++;
            }

            return -1;
        }

        public static void AddTeamTask(GameEntity product, GameContext gameContext, int teamId, TeamTask task)
        {
            var taskId = product.team.Teams[teamId].Tasks.Count;

            AddTeamTask(product, gameContext, teamId, taskId, task);
        }
        public static void AddTeamTask(GameEntity product, GameContext gameContext, int teamId, int taskId, TeamTask task)
        {
            var team = product.team.Teams[teamId];

            if (taskId >= team.Tasks.Count)
            {
                product.team.Teams[teamId].Tasks.Add(task);
            }
            else
            {
                try
                {
                    DisableTask(product, gameContext, teamId, taskId);
                    product.team.Teams[teamId].Tasks[taskId] = task;
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error on taskId: {taskId} / {product.team.Teams[teamId].Tasks.Count}" + taskId);
                }
            }

            if (task.IsFeatureUpgrade)
            {
                Products.UpgradeFeature(product, (task as TeamTaskFeatureUpgrade).NewProductFeature.Name, gameContext, team);
            }

            if (task.IsMarketingTask)
            {
                var channel = Markets.GetMarketingChannel(gameContext, (task as TeamTaskChannelActivity).ChannelId);

                if (!Marketing.IsCompanyActiveInChannel(product, channel))
                    Marketing.EnableChannelActivity(product, gameContext, channel);
            }

            if (task.IsHighloadTask || task.IsSupportTask)
            {
                var name = (task as TeamTaskSupportFeature).SupportFeature.Name;

                if (!product.supportUpgrades.Upgrades.ContainsKey(name))
                {
                    product.supportUpgrades.Upgrades[name] = 0;
                }

                product.supportUpgrades.Upgrades[name]++;
            }
        }

        public static SlotInfo GetSlotOfTeamTask(GameEntity product, TeamTask task)
        {
            for (var teamId = 0; teamId < product.team.Teams.Count; teamId++)
            {
                var t = product.team.Teams[teamId];
                for (var slotId = 0; slotId < t.Tasks.Count; slotId++)
                {
                    if (t.Tasks[slotId] == task)
                    {
                        return new SlotInfo { SlotId = slotId, TeamId = teamId };
                    }
                }
            }

            return null;
        }

        static void DisableTask(GameEntity product, GameContext gameContext, int teamId, int taskId) => DisableTask(product, gameContext, product.team.Teams[teamId].Tasks[taskId]);
        static void DisableTask(GameEntity product, GameContext gameContext, TeamTask task)
        {
            //Debug.Log($"Disabling task {task.ToString()} from {product.company.Name}");

            if (task.IsMarketingTask)
            {
                var activity = task as TeamTaskChannelActivity;

                var channel = Markets.GetMarketingChannels(gameContext).First(c => c.marketingChannel.ChannelInfo.ID == activity.ChannelId);
                Marketing.DisableChannelActivity(product, gameContext, channel);
            }

            if (task.IsFeatureUpgrade)
            {
                var activity = task as TeamTaskFeatureUpgrade;

                
                //var channel = Markets.GetMarketingChannels(gameContext).First(c => c.marketingChannel.ChannelInfo.ID == activity.ChannelId);
                //Products.DisableChannelActivity(product, gameContext, channel);
            }

            if (task.IsHighloadTask || task.IsSupportTask)
            {
                var activity = task as TeamTaskSupportFeature;

                var up = product.supportUpgrades.Upgrades;

                var name = activity.SupportFeature.Name;
                if (up.ContainsKey(name))
                {
                    up[name]--;
                }

                if (up[name] <= 0)
                {
                    up.Remove(name);
                }
            }
        }

        public static void RemoveTeamTask(GameEntity product, GameContext gameContext, TeamTask task)
        {
            var slot = GetSlotOfTeamTask(product, task);

            if (slot == null)
                return;

            RemoveTeamTask(product, gameContext, slot.TeamId, slot.SlotId);
        }

        public static void RemoveTeamTask(GameEntity product, GameContext gameContext, int teamId, int taskId)
        {
            //Debug.Log($"Remove Task: {taskId} from team {teamId}");

            if (product.team.Teams[teamId].Tasks.Count() > taskId)
            {
                DisableTask(product, gameContext, teamId, taskId);

                product.team.Teams[teamId].Tasks.RemoveAt(taskId);
            }
        }
    }
}
