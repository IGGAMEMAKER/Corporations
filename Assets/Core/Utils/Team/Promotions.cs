using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static int GetPromotionCost(TeamInfo teamInfo)
        {
            return 1;
            switch (teamInfo.Rank)
            {
                case TeamRank.Solo:
                    return 0;
                    break;
                case TeamRank.SmallTeam:
                    return 15;
                    break;
                case TeamRank.BigTeam:
                    return 25;
                    break;
                case TeamRank.Department:
                    return 50;
                    break;
                default:
                    return 100_000;
            }
        }

        public static bool IsTeamPromotable(GameEntity product, TeamInfo team)
        {
            return false;
            bool hasLeadManager = HasMainManagerInTeam(team);

            var managerPoints = product.companyResource.Resources.managerPoints;
            var promotionCost = GetPromotionCost(team);

            var enoughManagementPoints = managerPoints >= promotionCost;

            return hasLeadManager && Teams.IsFullTeam(team) && enoughManagementPoints && team.Rank < TeamRank.Department;
        }

        public static TeamRank GetNextTeamRank(TeamRank teamRank)
        {
            switch (teamRank)
            {
                case TeamRank.Solo:
                    return TeamRank.SmallTeam;

                case TeamRank.SmallTeam:
                    return TeamRank.BigTeam;

                default:
                    return TeamRank.Department;
            }
        }

        public static void Promote(GameEntity product, TeamInfo team)
        {
            return;
            var promotionCost = new TeamResource(0, GetPromotionCost(team), 0, 0, 0);

            if (Companies.IsEnoughResources(product, promotionCost))
            {
                team.Rank = GetNextTeamRank(team.Rank);

                team.Name = GenerateTeamName(product, team);
                team.Organisation = Mathf.Min(team.Organisation, 10);

                Companies.Pay(product, promotionCost, "Team Promotion");
            }
        }

    }
}
