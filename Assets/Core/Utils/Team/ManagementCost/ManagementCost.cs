using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static Bonus<float> GetManagerPointChange(GameEntity company, GameContext gameContext)
        {
            var teams = company.team.Teams;
        
            var bonus = new Bonus<float>("Point gain");

            bool teamsOnly = teams.Count > 3;

            foreach (var team in teams.Where(t => t.isIndependentTeam))
            {
                var b = GetTeamCostForParentTeam(team, company, gameContext, false);

                if (teamsOnly)
                {
                    bonus.Append(team.Name, b.Sum());
                }
                else
                {
                    bonus.AddBonus(b);
                }
            }

            return bonus;
        }

        public static Bonus<float> GetTeamCostForParentTeam(TeamInfo team, GameEntity company, GameContext gameContext, bool fullDescription = false)
        {
            var gain = GetLeaderGain(team, company, gameContext);

            // always positive
            var cost = GetDirectManagementCostOfTeam(team, company, gameContext);

            var directCost = cost.Sum();

            // always positive or 0
            var dependantTeams = GetDependantTeams(team, company);
            var dependantTeamCost = Math.Abs(dependantTeams.Sum(s => GetTeamCostForParentTeam(s, company, gameContext).Sum()));

            var totalCost = directCost + dependantTeamCost;

            var bonus = new Bonus<float>("Team Cost");

            if (team.isCoreTeam)
            {
                bonus.AddBonus(cost);
                /*bonus.Append("CEO", gain);
                bonus.Append($"Direct cost of Core team", -directCost);*/

                ApplyDependantTeamsBonus(bonus, team, company, gameContext);

                return bonus;
            }


            if (gain > totalCost)
            {
                // team is managed well and can be managed indirectly
                // except it is a core team (or independent team?)

                bonus.Append($"Management of {team.Name}", -GetIndirectManagementCostOfTeam(team, company));

                return bonus;
            }


            // team managed badly and will cause additional troubles in parent team
            var indirectCost = GetIndirectManagementCostOfTeam(team, company);

            if (fullDescription)
            {
                bonus
                    .Append("Manager", gain)
                    .Append("Team self cost", -directCost)
                    .Append("Indirect cost", -indirectCost);

                ApplyDependantTeamsBonus(bonus, team, company, gameContext);

                return bonus;
            }

            return bonus.Append($"Management cost", gain - totalCost - indirectCost);
        }


        public static void ApplyDependantTeamsBonus(Bonus<float> bonus, TeamInfo team, GameEntity company, GameContext gameContext)
        {
            foreach (var t in GetDependantTeams(team, company))
            {
                var cost = GetTeamCostForParentTeam(t, company, gameContext).Sum();

                bonus.Append(t.Name, -Math.Abs(cost));
            }
        }



        static GameEntity GetLeader(TeamInfo team, GameContext gameContext)
        {
            var role = GetMainManagerRole(team);
            var manager = GetWorkerByRole(role, team, gameContext);

            return manager;
        }

        static float GetLeaderGain(TeamInfo team, GameEntity company, GameContext gameContext)
        {
            var manager = GetLeader(team, gameContext);

            return GetLeaderGain(manager, company);
        }

        static float GetLeaderGain(GameEntity manager, GameEntity company)
        {
            var rating = GetEffectiveManagerRating(company, manager);
        
            return rating / 10f;
        }
    }
}
