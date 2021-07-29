using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static bool IsTaskSuitsTeam(TeamType teamType, TeamTask teamTask)
        {
            if (IsUniversalTeam(teamType))
            {
                return true;

                // universal teams cannot run datacenters
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
                return teamType == TeamType.ServersideTeam;

            return false;
        }

        public static bool AddTeamTask(GameEntity product, int date, GameContext gameContext, int teamId, TeamTask task)
        {
            var taskId = product.team.Teams[teamId].Tasks.Count;

            if (!HasFreeSlotForTeamTask(product, task) || !CanExecuteTeamTask(product, task, gameContext))
                task.IsPending = true;

            AddTeamTask(product, date, gameContext, teamId, taskId, task);

            return task.IsPending;
        }

        public static void AddTeamTask(GameEntity product, int date, GameContext gameContext, int teamId, int taskId, TeamTask task)
        {
            var team = product.team.Teams[teamId];

            if (taskId >= team.Tasks.Count)
            {
                // add new task
                product.team.Teams[teamId].Tasks.Add(task);
            }
            else
            {
                // replace old task
                DisableTask(product, gameContext, teamId, taskId);

                product.team.Teams[teamId].Tasks[taskId] = task;
            }

            InitializeTeamTaskIfNotPending(product, date, gameContext, task);
        }

        public static int GetFeatureUpgradeCost(GameEntity company, TeamTask teamTask)
        {
            var task = teamTask as TeamTaskFeatureUpgrade;
            var cost = Products.GetFeatureRating(company, task.NewProductFeature.Name);

            return 1 + (int)cost;
        }

        public static bool CanExecuteTeamTask(GameEntity company, TeamTask teamTask, GameContext gameContext)
        {
            if (teamTask.IsFeatureUpgrade)
            {
                var have = company.companyResource.Resources.ideaPoints;
                var cost = GetFeatureUpgradeCost(company, teamTask);

                return have >= cost;
            }

            if (teamTask.IsMarketingTask)
            {
                var payer = company.isFlagship ? Companies.GetPlayerCompany(gameContext) : company;
                var cost = Marketing.GetChannelCost(company, (teamTask as TeamTaskChannelActivity).ChannelId);

                return Economy.BalanceOf(payer) > cost;
            }

            return true;
        }


        public static void InitializeTeamTaskIfNotPending(GameEntity product, int date, GameContext gameContext, TeamTask task)
        {
            if (task.IsPending)
                return;

            task.StartDate = date;

            if (task.IsMarketingTask)
            {
                var channelId = (task as TeamTaskChannelActivity).ChannelId;
                var channel = Markets.GetMarketingChannel(gameContext, channelId);

                if (!Marketing.IsActiveInChannel(product, channelId))
                {
                    var cost = Marketing.GetChannelCost(product, channelId);
                    var payer = Companies.GetPayer(product, gameContext);

                    if (Companies.Pay(payer, cost, "Marketing " + channel))
                        Marketing.EnableChannelActivity(product, channel);
                }
            }
        }

        public static void ProcessTeamTaskIfNotPending(GameEntity p, int date, TeamTask task, ref List<SlotInfo> removableTasks, int slotId, int teamId)
        {
            if (task.IsPending)
                return;
            
            if (task is TeamTaskChannelActivity)
            {
                // channel tookout
            }
        }

        static void DisableTask(GameEntity product, GameContext gameContext, int teamId, int taskId) => DisableTask(product, gameContext, product.team.Teams[teamId].Tasks[taskId]);
        static void DisableTask(GameEntity product, GameContext gameContext, TeamTask task)
        {
            //Debug.Log($"Disabling task {task.ToString()} from {product.company.Name}");

            if (task.IsMarketingTask)
            {
                var activity = task as TeamTaskChannelActivity;

                var channel = Markets.GetMarketingChannel(gameContext, activity.ChannelId);
                
                Marketing.DisableChannelActivity(product, channel);
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

                var upgrades = product.supportUpgrades.Upgrades;

                var name = activity.SupportFeature.Name;
                if (upgrades.ContainsKey(name))
                {
                    upgrades[name]--;
                }

                if (upgrades[name] <= 0)
                {
                    upgrades.Remove(name);
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

            var tasks = product.team.Teams[teamId].Tasks;

            if (tasks.Count() > taskId)
            {
                if (!tasks[taskId].IsPending)
                    DisableTask(product, gameContext, teamId, taskId);

                tasks.RemoveAt(taskId);
            }
        }
    }
}
