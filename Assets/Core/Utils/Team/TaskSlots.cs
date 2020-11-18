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
            return GetSlotsForTaskType(product, task) > 0;
        }

        public static int GetSlotsForTaskType(GameEntity product, TeamTask task)
        {
            return GetMaxSlotsForTaskType(product, task) - GetAllSameTaskTypeSlots(product, task);
        }

        public static int GetMaxSlotsForTaskType(GameEntity product, TeamTask task)
        {
            return product.team.Teams.Sum(t => GetSlotsForTask(t, task));
        }

        public static int GetTeamTasks(TeamInfo team)
        {
            switch (team.Rank)
            {
                case TeamRank.Solo:         return 1 * 4;
                case TeamRank.SmallTeam:    return 2 * 4;
                case TeamRank.BigTeam:      return 5 * 4;
                case TeamRank.Department:   return 10 * 4;

                default: return -1000;
            }
        }

        public static int GetSlotsForTask(TeamInfo team, TeamTask task)
        {
            if (IsTaskSuitsTeam(team.TeamType, task))
                return GetTeamTasks(team);

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
            return product.team.Teams[0].Tasks.Count(t => task.AreSameTypeTasks(t));
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

        //
        public static int GetAmountOfUpgradingFeatures(GameEntity product, GameContext gameContext)
        {
            var features = Products.GetAllFeaturesForProduct(product);

            int upgrading = 0;
            foreach (var f in features)
            {
                if (IsUpgradingFeature(product, gameContext, f.Name))
                    upgrading++;
            }

            return upgrading;
        }

        public static bool IsUpgradingFeature(GameEntity product, GameContext Q, string featureName)
        {
            var cooldownName = $"company-{product.company.Id}-upgradeFeature-{featureName}";

            return Cooldowns.HasCooldown(Q, cooldownName, out SimpleCooldown simpleCooldown);
        }
    }
}
