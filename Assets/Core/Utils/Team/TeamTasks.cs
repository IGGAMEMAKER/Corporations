using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static bool AddTeamTask(GameEntity product, int date, GameContext gameContext, int teamId, TeamTask task)
        {
            var taskId = product.team.Teams[teamId].Tasks.Count;

            if (!CanExecuteTeamTask(product, task, gameContext))
                task.IsPending = true;

            if (!task.IsPending)
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

        public static bool IsEnoughResourcesForTask(GameEntity company, TeamTask teamTask, GameContext gameContext)
        {
            var cost = GetTaskCost(company, teamTask, gameContext);

            return Companies.IsEnoughResources(company, cost);
        }
        public static TeamResource GetTaskCost(GameEntity company, TeamTask teamTask, GameContext gameContext)
        {
            if (teamTask.IsFeatureUpgrade)
            {
                var cost = GetFeatureUpgradeCost(company, teamTask);

                return new TeamResource(0, 0, 0, cost, 0);
            }

            if (teamTask.IsMarketingTask)
            {
                var cost = Marketing.GetChannelCost(company, (teamTask as TeamTaskChannelActivity).ChannelId);

                return new TeamResource(cost);
            }

            return new TeamResource(0);
        }

        public static bool CanExecuteTeamTask(GameEntity company, TeamTask teamTask, GameContext gameContext)
        {
            var taskCost = GetTaskCost(company, teamTask, gameContext);

            if (teamTask.IsFeatureUpgrade)
                return Companies.IsEnoughResources(company, taskCost);

            if (teamTask.IsMarketingTask)
            {
                var payer = Companies.GetPayer(company, gameContext);

                return Companies.IsEnoughResources(payer, taskCost);
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
                    {
                        Marketing.EnableChannelActivity(product, channel);

                        var gain = Marketing.GetChannelClientGain(product, channelId);
                        Marketing.AddClients(product, gain);

                        var duration = Marketing.GetCampaignDuration(product, gain);
                        task.EndDate = date + duration;
                    }
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

                // campaign expired
                if (task.EndDate <= date)
                {
                    removableTasks.Add(new SlotInfo { SlotId = slotId, TeamId = teamId });
                }
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
