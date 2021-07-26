using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static float GetIndirectManagementCostOfTeam(TeamInfo team, GameEntity company)
        {
            var flatness = GetPolicyValueModified(company, CorporatePolicy.DecisionsManagerOrTeam, 1f, 0.5f, 0.25f);

            return GetIndirectManagementCostOfTeam(team) * flatness;
        }

        public static int GetIndirectManagementCostOfTeam(TeamInfo team)
        {
            return 2;
            switch (team.Rank)
            {
                case TeamRank.Solo: return 1;
                case TeamRank.SmallTeam: return 2;
                case TeamRank.BigTeam: return 3;
                case TeamRank.Department: return 4;

                default: return 0;
            }
        }
    }
}
