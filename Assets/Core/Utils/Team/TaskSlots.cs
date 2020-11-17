using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public class SlotInfo
    {
        public int TeamId;
        public int SlotId;
    }

    public static partial class Teams
    {
        public static int GetAmountOfPossibleChannelsByTeamType(TeamInfo team)
        {
            switch (team.TeamType)
            {
                case TeamType.BigCrossfunctionalTeam: return 3;
                case TeamType.CrossfunctionalTeam: return 2;
                case TeamType.SmallCrossfunctionalTeam: return 1;
                case TeamType.MarketingTeam: return 2;

                default: return 0;
            }
        }

        public static int GetAmountOfPossibleFeaturesByTeamType(TeamInfo team)
        {
            switch (team.TeamType)
            {
                case TeamType.BigCrossfunctionalTeam: return 3;
                case TeamType.CrossfunctionalTeam: return 2;
                case TeamType.SmallCrossfunctionalTeam: return 1;
                case TeamType.DevelopmentTeam: return 2;

                default: return 0;
            }
        }

        public static int GetMaxSlotsForTaskType(GameEntity product, TeamTask task)
        {
            return product.team.Teams.Sum(t => (IsTaskSuitsTeam(t.TeamType, task) ? 1 : 0) * t.ID);
        }

        public static int GetActiveSlots(GameEntity product, TeamTask task)
        {
            return product.team.Teams[0].Tasks.Count(t => task.AreSameTypeTasks(t));
        }

        public static int GetFreeSlotsForTaskType(GameEntity product, TeamTask task)
        {
            return GetMaxSlotsForTaskType(product, task) - GetActiveSlots(product, task);
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
