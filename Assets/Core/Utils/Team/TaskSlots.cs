using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
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

        public static int GetMaxSlotsForTaskType(GameEntity product, TeamTask task)
        {
            return product.team.Teams.Count(t => IsTaskSuitsTeam(t.TeamType, task));
        }

        public static int GetAmountOfSameTypeTasksThatAreActive(GameEntity product, TeamTask task)
        {
            return product.team.Teams[0].Tasks.Count(t => t.);
        }

        public static int GetFreeSlotsForTaskType(GameEntity product, TeamTask task)
        {

        }
    }
}
