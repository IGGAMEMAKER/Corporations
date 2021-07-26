using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static bool IsCanReceiveTeams(TeamInfo team)
        {
            return Teams.IsFullTeam(team) && team.Rank == TeamRank.Department;
        }

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

        public static bool IsTeamDependsAlready(GameEntity company, TeamInfo owner, TeamInfo target, bool recursively)
        {
            if (target.ParentID == owner.ID)
                return true;

            if (recursively)
            {
                var dependantTeams = GetDependantTeams(owner, company);
                foreach (var teamInfo in dependantTeams)
                {
                    if (IsTeamDependsAlready(company, teamInfo, target, true))
                        return true;
                }
            }

            return false;
        }

        public static void AttachTeamToTeam(TeamInfo dependant, TeamInfo owner)
        {
            dependant.ParentID = owner.ID;
        }

        public static IEnumerable<TeamInfo> GetDependantTeams(TeamInfo owner, GameEntity company)
        {
            return company.team.Teams.Where(t => t.ParentID == owner.ID);
        }

        public static void DetachTeamFromTeam(TeamInfo team)
        {
            team.ParentID = -1;
        }
    }
}
