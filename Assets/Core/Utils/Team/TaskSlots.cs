using System.Linq;

namespace Assets.Core
{
    public class SlotInfo
    {
        public int TeamId;
        public int SlotId;
    }

    public static partial class Teams
    {
        public static bool HasFreeSlotForTeamTask(GameEntity product, TeamTask task)
        {
            return true;
            var taskCost = GetTaskSlotCost(product, task);

            if (taskCost == 0)
                return true;

            return GetOverallSlotsForTaskType(product, task) >= taskCost;
        }

        public static int GetTaskSlotCost(GameEntity product, TeamTask task)
        {
            if (task.IsFeatureUpgrade || task.IsMarketingTask)
                return 1;

            if (task.IsHighloadTask && (task as TeamTaskSupportFeature).SupportFeature.SupportBonus.Max >= 2_000_000)
                return 1;

            return 0;
        }

        public static int GetOverallSlotsForTaskType(GameEntity product, TeamTask task)
        {
            return GetMaxSlotsForTaskType(product, task) - GetActiveSameTaskTypeSlots(product, task); // GetAllActiveTaskSlots(product);
        }

        public static int GetMaxSlotsForTaskType(GameEntity product, TeamTask task)
        {
            return product.team.Teams.Sum(t => GetSlotsForTask(t, task));
        }

        public static int GetTeamTasks(TeamInfo team)
        {
            switch (team.Rank)
            {
                case TeamRank.Solo:         return 1;
                case TeamRank.SmallTeam:    return 2;
                case TeamRank.BigTeam:      return 3; // 5
                case TeamRank.Department:   return 6; // 10

                default: return -1000;
            }
        }

        public static TeamTask GetDevelopmentTaskMockup()
        {
            return new TeamTaskFeatureUpgrade(null);
            //return new TeamTaskFeatureUpgrade(new NewProductFeature("x", new System.Collections.Generic.List<int>()));
        }

        public static TeamTask GetMarketingTaskMockup()
        {
            return new TeamTaskChannelActivity(0, 0);
        }

        public static int GetTeamFeatureSlots(TeamInfo team)
        {
            return team.Roles.Values.Count(r => r == WorkerRole.Programmer || r == WorkerRole.TeamLead);
        }
        public static int GetTeamMarketingSlots(TeamInfo team)
        {
            return team.Roles.Values.Count(r => r == WorkerRole.Marketer || r == WorkerRole.MarketingLead);
        }

        public static int GetSlotsForTask(TeamInfo team, TeamTask task)
        {
            if (IsTaskSuitsTeam(team.TeamType, task))
            {
                if (IsUniversalTeam(team.TeamType))
                    return GetTeamTasks(team);
                
                return GetTeamTasks(team) * 2;
            }

            // group by team type
            // group by task type

            return 0;
        }

        public static int GetPendingMarketingChannelsAmount(GameEntity product)
        {
            return product.team.Teams[0].Tasks.Count(t => t.IsMarketingTask && t.IsPending);
        }
        public static int GetPendingServersAmount(GameEntity product)
        {
            return product.team.Teams[0].Tasks.Count(t => t.IsHighloadTask && t.IsPending);
        }
        public static int GetPendingFeaturesAmount(GameEntity product)
        {
            return product.team.Teams[0].Tasks.Count(t => t.IsFeatureUpgrade && t.IsPending);
        }

        public static int GetPendingSameTypeTaskAmount(GameEntity product, TeamTask task)
        {
            if (task.IsFeatureUpgrade)
                return GetPendingFeaturesAmount(product);

            if (task.IsMarketingTask)
                return GetPendingMarketingChannelsAmount(product);

            if (task.IsHighloadTask)
                return GetPendingServersAmount(product);

            return 0;
        }


        public static int GetAllSameTaskTypeSlots(GameEntity product, TeamTask task)
        {
            return product.team.Teams[0].Tasks.Count(task.AreSameTypeTasks);
        }

        public static int GetActiveSameTaskTypeSlots(GameEntity product, TeamTask task)
        {
            return product.team.Teams[0].Tasks.Count(t => task.AreSameTypeTasks(t) && !t.IsPending);
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
    }
}
