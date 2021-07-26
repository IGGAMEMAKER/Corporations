using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static IEnumerable<TeamInfo> GetMergingCandidates(TeamInfo team, GameEntity company)
        {
            return company.team.Teams.Where(t => IsCanMergeTeams(company, team, t));
        }
        public static bool IsHasMergeCandidates(TeamInfo team, GameEntity company)
        {
            return GetMergingCandidates(team, company).Any();
        }

        public static bool IsCanMergeTeams(GameEntity company, TeamInfo owner, TeamInfo target)
        {
            if (!IsCanReceiveTeams(owner))
                return false;

            bool noAttachToSelf = target.ID != owner.ID;

            return target.TeamType == owner.TeamType
                && target.isAttachable
                && noAttachToSelf
                && !IsTeamDependsAlready(company, owner, target, true)
                && !IsTeamDependsAlready(company, target, owner, true); //  target.ParentID != owner.ID;
        }
    }
}
