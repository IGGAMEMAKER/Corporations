using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Teams
    {
        // ------------------- Management cost
        
        public static Bonus<float> GetManagerPointChange(GameEntity company, GameContext gameContext)
        {
            var teams = company.team.Teams;
        
            var bonus = new Bonus<float>("Point gain");

            bool teamsOnly = teams.Count > 3;

            foreach (var team in teams.Where(t => t.isIndependentTeam))
            {
                var b = GetTeamManagementBonus(team, company, gameContext, true);

                if (teamsOnly)
                {
                    bonus.Append(team.Name, b.Sum());
                }
                else
                {
                    bonus.Append(b);
                }
            }

            return bonus;
        }

        public static float GetPolicyValueModified(GameEntity company, CorporatePolicy policy, float min,
            float centerValue,
            float max)
        {
            var flatness = Companies.GetPolicyValue(company, policy);

            var center = 5;

            // ---- structure
            var multiplier = centerValue;
            if (flatness < center)
                multiplier = min;

            if (flatness > center)
                multiplier = max;

            return multiplier;
        }

        public static bool IsTeamSelfManageable(TeamInfo team, GameEntity company, GameContext gameContext, bool recursively = false)
        {
            if (!HasMainManagerInTeam(team))
                return false;
            
            var managementCost = GetDirectManagementCostOfTeam(team, company, gameContext);
            var gain = GetTeamManagementGain(team, company, gameContext);
            
            return gain >= managementCost;
        }

        public static void ApplyDependantTeamsBonus(Bonus<float> bonus, TeamInfo team, GameEntity company,
            GameContext gameContext)
        {
            var dependantTeams = GetDependantTeams(team, company);

            foreach (var t in dependantTeams)
            {
                bonus.Append(t.Name, -Math.Abs(GetTeamCostForParentTeam(t, company, gameContext).Sum()));
            }
        }
        
        public static Bonus<float> GetTeamCostForParentTeam(TeamInfo team, GameEntity company, GameContext gameContext, bool fullDescription = false)
        {
            var gain = GetTeamManagementGain(team, company, gameContext);

            // always positive
            var directCost = GetDirectManagementCostOfTeam(team, company, gameContext);
            
            // always positive or 0
            var dependantTeams = GetDependantTeams(team, company);
            var dependantTeamCost = Math.Abs(dependantTeams
                .Sum(s => GetTeamCostForParentTeam(s, company, gameContext).Sum()));

            var totalCost = directCost + dependantTeamCost;
            var indirectCost = GetIndirectManagementCostOfTeam(team, company);

            var bonus = new Bonus<float>("Team Cost");
            
            if (team.isCoreTeam)
            {
                bonus.Append("CEO", gain);
                bonus.Append($"Direct cost of Core {team.Rank}", -directCost);

                ApplyDependantTeamsBonus(bonus, team, company, gameContext);
                // bonus.AppendAndHideIfZero("Dependant teams", -dependantTeamCost);

                return bonus;
            }
            
            if (gain > totalCost)
            {
                // team is managed well and can be managed indirectly
                // except it is a core team (or independent team?)

                return bonus.Append($"Indirect management of {team.Rank}", -indirectCost);
            }
            else
            {
                // team managed badly and will cause additional troubles in parent team
                
                if (fullDescription)
                {
                    bonus
                        .Append("Manager", gain)
                        .Append("Team self cost", -directCost)
                        .Append("Indirect cost", -indirectCost);
                    
                    ApplyDependantTeamsBonus(bonus, team, company, gameContext);

                    return bonus;
                }
                else
                {
                    return bonus.Append($"Management cost", gain - totalCost - indirectCost);
                }
            }
        }

        public static Bonus<float> GetTeamManagementBonus(TeamInfo team, GameEntity company, GameContext gameContext,
            bool shortDescription = false)
        {
            var bonus = new Bonus<float>("points");

            return GetTeamCostForParentTeam(team, company, gameContext, !shortDescription);

            var needsHelp = !IsTeamSelfManageable(team, company, gameContext);

            bool isDirectManagement = team.isCoreTeam || needsHelp;

            if (team.isCoreTeam)
                return GetTeamCostForParentTeam(team, company, gameContext);

            if (isDirectManagement)
            {
                // if not managed properly
                // spend points from parent team / CEO

                var gain = GetTeamManagementGain(team, company, gameContext);

                var directMaintenance = GetDirectManagementCostOfTeam(team, company, gameContext);


                foreach (var dependantTeam in GetDependantTeams(team, company))
                {
                    var b = GetTeamCostForParentTeam(dependantTeam, company, gameContext);

                    if (!shortDescription)
                        bonus.Append(b);
                    
                    directMaintenance += b.Sum();
                }

                if (shortDescription)
                {
                    bonus.AppendAndHideIfZero("Direct management in " + team.Name, gain - directMaintenance);
                }
                else
                {
                    var role = GetMainManagerRole(team);

                    bonus.AppendAndHideIfZero(Humans.GetFormattedRole(role), gain);
                    bonus.AppendAndHideIfZero($"Base maintenance for {team.Rank}", -directMaintenance);
                }
            }


            if (!isDirectManagement)
            {
                ApplyIndirectManagementBonus(company,team, bonus);
            }

            return bonus;
        }

        // public static Bonus<float> GetTeamManagementBonus(TeamInfo team, GameEntity company, GameContext gameContext,
        //     bool shortDescription = false)
        // {
        //     var bonus = new Bonus<float>("points");
        //
        //     var needsHelp = !IsTeamSelfManageable(team, company, gameContext);
        //
        //     bool isDirectManagement = team.isCoreTeam || needsHelp;
        //
        //     if (isDirectManagement)
        //     {
        //         // if not managed properly
        //         // spend points from parent team / CEO
        //
        //         var gain = GetTeamManagementGain(team, company, gameContext);
        //
        //         var directMaintenance = GetDirectManagementCostOfTeam(team, company);
        //
        //
        //         foreach (var dependantTeam in GetDependantTeams(team, company))
        //         {
        //             var b = GetTeamCostForParentTeam(dependantTeam, company, gameContext);
        //
        //             if (!shortDescription)
        //                 bonus.Append(b);
        //             
        //             directMaintenance += b.Sum();
        //         }
        //
        //         if (shortDescription)
        //         {
        //             bonus.AppendAndHideIfZero("Direct management in " + team.Name, gain - directMaintenance);
        //         }
        //         else
        //         {
        //             var role = GetMainManagerRole(team);
        //
        //             bonus.AppendAndHideIfZero(Humans.GetFormattedRole(role), gain);
        //             bonus.AppendAndHideIfZero($"Base maintenance for {team.Rank}", -directMaintenance);
        //         }
        //     }
        //
        //
        //     if (!isDirectManagement)
        //     {
        //         ApplyIndirectManagementBonus(company,team, bonus);
        //     }
        //
        //     return bonus;
        // }

        public static void ApplyIndirectManagementBonus(GameEntity company, TeamInfo team, Bonus<float> bonus)
        {
            var indirectMaintenance = GetIndirectManagementCostOfTeam(team, company);

            bonus.Append($"Indirect management for {team.Rank}", -indirectMaintenance);
        }

        static float GetTeamManagementGain(TeamInfo team, GameEntity company, GameContext gameContext)
        {
            var role = GetMainManagerRole(team);
            var manager = GetWorkerByRole(role, team, gameContext);

            var rating = GetEffectiveManagerRating(company, manager);
        
            return rating / 10f;
        }

        // TODO rewrite for more performance
        public static IEnumerable<GameEntity> GetStaffInTeam(TeamInfo team, GameContext gameContext)
        {
            return team.Managers.Select(humanId => Humans.Get(gameContext, humanId));
        }

        public static float GetDirectManagementCostOfTeam(TeamInfo team, GameEntity company, GameContext gameContext)
        {
            var processes = GetPolicyValueModified(company, CorporatePolicy.PeopleOrProcesses, 1f, 0.5f, 0.25f);

            return GetDirectManagementCostOfTeam(team, gameContext) * processes;
        }
        public static float GetDirectManagementCostOfTeam(TeamInfo team, GameContext gameContext)
        {
            var managers = GetStaffInTeam(team, gameContext);

            var ratings = managers.Select(h => 150 - Humans.GetRating(h)); // 50...90

            return ratings.Sum() / 100f;

            switch (team.Rank)
            {
                case TeamRank.Solo: return team.isCoreTeam ? 4 : C.MANAGEMENT_COST_SOLO;
                case TeamRank.SmallTeam: return C.MANAGEMENT_COST_SMALL_TEAM;
                case TeamRank.BigTeam: return C.MANAGEMENT_COST_BIG_TEAM;
                case TeamRank.Department: return C.MANAGEMENT_COST_DEPARTMENT;

                default: return 0;
            }
        }

        public static float GetIndirectManagementCostOfTeam(TeamInfo team, GameEntity company)
        {
            var flatness  = GetPolicyValueModified(company, CorporatePolicy.DecisionsManagerOrTeam, 1f, 0.5f, 0.25f);

            return GetIndirectManagementCostOfTeam(team) * flatness;
        }
        public static int GetIndirectManagementCostOfTeam(TeamInfo team)
        {
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
