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

        public static void AddTeamTask(GameEntity product, int date, GameContext gameContext, int teamId, TeamTask task)
        {
            var taskId = product.team.Teams[teamId].Tasks.Count;

            var slots = GetOverallSlotsForTaskType(product, task);

            if (!HasFreeSlotForTeamTask(product, task))
                task.IsPending = true;

            AddTeamTask(product, date, gameContext, teamId, taskId, task);
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

        public static void InitializeTeamTaskIfNotPending(GameEntity product, int date, GameContext gameContext, TeamTask task)
        {
            if (task.IsPending)
                return;

            task.StartDate = date;

            if (task.IsFeatureUpgrade)
            {
                Products.AddFeatureCooldown(product, task, date);
            }

            if (task.IsMarketingTask)
            {
                var channel = Markets.GetMarketingChannel(gameContext, (task as TeamTaskChannelActivity).ChannelId);

                if (!Marketing.IsActiveInChannel(product, channel))
                    Marketing.EnableChannelActivity(product, channel);
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

        public static void ProcessTeamTaskIfNotPending(GameEntity p, int date, TeamTask task, ref List<SlotInfo> removableTasks, int slotId, int teamId)
        {
            if (task.IsPending)
                return;
            
            if (task is TeamTaskFeatureUpgrade upgrade)
            {
                var featureName = upgrade.NewProductFeature.Name;

                if (date >= task.EndDate)
                {
                    Products.IncreaseFeatureLevel(p, featureName);

                    task.StartDate = date;
                    Products.AddFeatureCooldown(p, task, date);
                }

                // ----------------------- STOP IF REACHED CAP ------------------------------------

                if (Products.GetFeatureRating(p, featureName) >= Products.GetFeatureRatingCap(p))
                {
                    removableTasks.Add(new SlotInfo {SlotId = slotId, TeamId = teamId});
                }
            }

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

                var channel = Markets.GetAllMarketingChannels(gameContext)
                    .First(c => c.marketingChannel.ChannelInfo.ID == activity.ChannelId);
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
