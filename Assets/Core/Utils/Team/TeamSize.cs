﻿using System;
using System.Linq;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static int GetTotalEmployees(GameEntity e)
        {
            return e.team.Teams.Sum(GetMaxTeamSize);
        }

        public static int GetMaxTeamSize(TeamInfo team) => GetMaxTeamSize(team.Rank);

        public static int GetMaxTeamSize(TeamRank rank)
        {
            return 1;
            switch (rank)
            {
                case TeamRank.Solo: return 1;
                case TeamRank.SmallTeam: return 8;
                case TeamRank.BigTeam: return 20;
                case TeamRank.Department: return 100;

                default: return 100_000;
            }
        }

        public static int GetPromotionCost(TeamRank teamType)
        {
            switch (teamType)
            {
                case TeamRank.Solo:
                    return C.PROMOTION_POINTS_TO_SOLO_TEAM;
                    break;
                case TeamRank.SmallTeam:
                    return C.PROMOTION_POINTS_TO_SMALL_TEAM;
                    break;
                case TeamRank.BigTeam:
                    return C.PROMOTION_POINTS_TO_BIG_TEAM;
                    break;
                case TeamRank.Department:
                    return C.PROMOTION_POINTS_TO_DEPARTMENT;
                    break;
                default:
                    return 100_000;
            }
        }

        public static bool IsCanAddMoreTeams(GameEntity company, GameContext gameContext)
        {
            return true;
            var teamCost = GetPromotionCost(TeamRank.Solo);
            
            return Companies.IsEnoughResources(company, new TeamResource(0, teamCost, 0, 0, 0));
        }
    }
}