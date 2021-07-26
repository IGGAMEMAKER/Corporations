using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static void AddTeam(GameEntity company, GameContext gameContext, TeamType teamType, int parentId)
        {
            AddTeam(company, gameContext, teamType);

            var team = company.team.Teams.Last();
            AttachTeamToTeam(team, parentId);
        }

        public static void AddTeam(GameEntity company, GameContext gameContext, TeamType teamType)
        {
            if (!IsCanAddMoreTeams(company, gameContext))
                return;

            var team = new TeamInfo
            {
                TeamType = teamType,
                Tasks = new List<TeamTask>(),

                Managers = new List<int>(),
                Roles = new Dictionary<int, WorkerRole>(),

                ManagerTasks = new List<ManagerTask> {ManagerTask.None, ManagerTask.None, ManagerTask.None},

                HiringProgress = 0,
                Workers = 0,
                Organisation = 0,
                ID = company.team.Teams.Count,
                Rank = TeamRank.Solo,

                ManagedBadly = false,

                TooManyLeaders = false
            };

            company.team.Teams.Add(team);

            team.Name = GenerateTeamName(company, team);
        }

        public static string GenerateTeamName(GameEntity company, TeamInfo team)
        {
            var prefix = GetFormattedTeamType(team);

            bool isCoreTeam = company.team.Teams.Count == 0;

            if (isCoreTeam)
                return prefix;

            return $"{prefix} #{team.ID}";
        }

        public static void RemoveTeam(GameEntity company, GameContext gameContext, int teamId)
        {
            var team = company.team.Teams[teamId];
            var tasks = team.Tasks;

            while (tasks.Count > 0)
            {
                RemoveTeamTask(company, gameContext, teamId, 0);
            }

            var dependantTeams = GetDependantTeams(team, company);
            foreach (var t in dependantTeams)
            {
                DetachTeamFromTeam(t);
            }
            // while (team.AttachedTeams.Any())
            // {
            //     team.AttachedTeams[0].
            // }

            company.team.Teams.RemoveAt(teamId);

            int id = 0;
            foreach (var t in company.team.Teams)
            {
                t.ID = id++;
            }

            Debug.Log("Team removed!");
        }

        static void ReplaceTeam(GameEntity company, GameContext gameContext, TeamComponent t)
        {
            company.ReplaceTeam(t.Morale, t.Organisation, t.Managers, t.Workers, t.Teams, t.Salaries);

            Economy.UpdateSalaries(company, gameContext);
        }

        // other ----------
        public static bool IsUniversalTeam(TeamType teamType) =>
            new[] {TeamType.CrossfunctionalTeam}.Contains(teamType);

        public static string GetFormattedTeamType(TeamInfo team) => GetFormattedTeamType(team.TeamType, team.Rank, team.isCoreTeam ? "Core team" : "");
        public static string GetFormattedTeamType(TeamType teamType, TeamRank rank, string formattedName = "")
        {
            if (formattedName.Length == 0)
                formattedName = GetFormattedTeamType(teamType);

            switch (rank)
            {
                /*case TeamRank.Solo:
                    return $"Solo {formattedName}";

                case TeamRank.SmallTeam:
                    return $"Small {formattedName}";

                case TeamRank.BigTeam:
                    return $"Big {formattedName}";*/

                case TeamRank.Department:
                    if (formattedName.Contains("Core"))
                        return "Core Department";
                    
                    switch (teamType)
                    {
                        case TeamType.CrossfunctionalTeam:
                            return "Crossfunctional Department";

                        case TeamType.DevelopmentTeam:
                            return "Development Department";

                        case TeamType.MarketingTeam:
                            return "Marketing Department";

                        case TeamType.MergeAndAcquisitionTeam:
                            return "M&A Department";

                        case TeamType.ServersideTeam:
                            return "Serverside Department";

                        case TeamType.SupportTeam:
                            return "Support Department";

                        default:
                            return teamType + " DEPARTMENT";
                    }

                default:
                    return formattedName;
                    //return rank + " " + formattedName;
            }
        }
        public static string GetFormattedTeamType(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.CrossfunctionalTeam: return "Universal team";

                case TeamType.DevelopmentTeam: return "Development team";
                case TeamType.MarketingTeam: return "Marketing team";
                case TeamType.MergeAndAcquisitionTeam: return "M&A team";
                case TeamType.SupportTeam: return "Support team";

                case TeamType.ServersideTeam: return "Serverside team";

                default: return teamType.ToString();
            }
        }


        public static bool IsFullTeam(TeamInfo team)
        {
            return true;
            return team.Workers >= Teams.GetMaxTeamSize(team);
        }

        public static bool IsNeverHiredEmployees(GameEntity product)
        {
            bool isFirstTeam = product.team.Teams.Count == 1;

            return isFirstTeam && product.team.Teams[0].Managers.Count <= 1;
        }

        public static bool IsTeamNeedsAttention(GameEntity product, TeamInfo team, GameContext gameContext)
        {
            bool hasNoManager = !HasMainManagerInTeam(team);

            bool hasDisloyalManagers = team.Managers
                .Select(m => Humans.Get(gameContext, m))
                .Any(h => h.humanCompanyRelationship.Morale < 40 && GetLoyaltyChangeForManager(h, team, product) < 0);

            return IsFullTeam(team) && (hasNoManager || hasDisloyalManagers || IsTeamPromotable(product, team));
        }

        public static void ToggleCrunching(GameEntity c)
        {
            c.isCrunching = !c.isCrunching;
        }
    }
}