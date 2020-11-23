using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
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

                ManagerTasks = new List<ManagerTask> { ManagerTask.None, ManagerTask.None, ManagerTask.None },

                HiringProgress = 0,
                Workers = 0,
                Organisation = 0,
                ID = company.team.Teams.Count,

                TooManyLeaders = false
            };

            company.team.Teams.Add(team);

            team.Name = GenerateTeamName(company, team);

            //return company.team.Teams.Count - 1;
        }

        public static string GenerateTeamName(GameEntity company, TeamInfo team)
        {
            var prefix = GetFormattedTeamType(team);

            bool isCoreTeam = company.team.Teams.Count == 0;

            if (isCoreTeam)
                return prefix;

            return $"{prefix} #{team.ID}";
        }

        public static bool IsTeamPromotable(GameEntity product, TeamInfo team, GameContext Q)
        {
            bool hasLeadManager = HasMainManagerInTeam(team, Q, product);
            var organisation = team.Organisation;

            return hasLeadManager && organisation >= 100 && team.Rank < TeamRank.Department;
        }

        public static void Promote(GameEntity product, TeamInfo team)
        {
            switch (team.Rank)
            {
                case TeamRank.Solo:
                    team.Rank = TeamRank.SmallTeam;
                    break;

                case TeamRank.SmallTeam:
                    team.Rank = TeamRank.BigTeam;
                    break;

                case TeamRank.BigTeam:
                    team.Rank = TeamRank.Department;
                    break;
            }

            team.Name = GenerateTeamName(product, team);
            team.Organisation = Mathf.Min(team.Organisation, 10);
        }

        public static void RemoveTeam(GameEntity company, GameContext gameContext, int teamId)
        {
            var tasks = company.team.Teams[teamId].Tasks;

            while (tasks.Count > 0)
            {
                RemoveTeamTask(company, gameContext, teamId, 0);
            }

            company.team.Teams.RemoveAt(teamId);

            int id = 0;
            foreach (var t in company.team.Teams)
            {
                t.ID = id++;
            }

            Debug.Log("Team removed!");
        }

        private static void ReplaceTeam(GameEntity company, GameContext gameContext, TeamComponent t)
        {
            company.ReplaceTeam(t.Morale, t.Organisation, t.Managers, t.Workers, t.Teams, t.Salaries);

            Economy.UpdateSalaries(company, gameContext);
        }

        // other ----------

        public static void ToggleCrunching(GameEntity c)
        {
            c.isCrunching = !c.isCrunching;
        }

        public static Bonus<int> GetOrganisationChanges(TeamInfo teamInfo, GameEntity product, GameContext gameContext)
        {
            WorkerRole managerTitle = GetMainManagerForTheTeam(teamInfo);

            var rating = GetEffectiveManagerRating(gameContext, product, managerTitle, teamInfo);

            var managerFocus = teamInfo.ManagerTasks.Count(t => t == ManagerTask.Organisation);

            bool NoCeo = rating < 1;

            // count team size: small teams organise faster, big ones: slower

            return new Bonus<int>("Organisation change")
                .AppendAndHideIfZero("No " + managerTitle.ToString(), NoCeo ? -30 : 0)
                .AppendAndHideIfZero(managerTitle.ToString() + " efforts", rating)
                .AppendAndHideIfZero("Manager focus on Organisation", NoCeo ? 0 : managerFocus * 10)
                ;
        }

        public static bool IsUniversalTeam(TeamType teamType) => new TeamType[] { TeamType.CrossfunctionalTeam }.Contains(teamType);

        public static string GetFormattedTeamType(TeamInfo team) => GetFormattedTeamType(team.TeamType, team.Rank, team.ID == 0 ? "Core team" : "");
        public static string GetFormattedTeamType(TeamType teamType, TeamRank rank, string formattedName = "")
        {
            if (formattedName.Length == 0)
                formattedName = GetFormattedTeamType(teamType);

            switch (rank)
            {
                case TeamRank.Solo:
                    return $"Solo {formattedName}";

                case TeamRank.SmallTeam:
                    return $"Small {formattedName}";

                case TeamRank.BigTeam:
                    return $"Big {formattedName}";

                case TeamRank.Department:
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
                            return teamType.ToString() + " DEPARTMENT";
                    }

                default:
                    return rank.ToString() + " " + formattedName;
            }
        }

        public static string GetFormattedTeamType(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.CrossfunctionalTeam:      return "Universal team";

                case TeamType.DevelopmentTeam:          return "Development team";
                case TeamType.MarketingTeam:            return "Marketing team";
                case TeamType.MergeAndAcquisitionTeam:  return "M&A team";
                case TeamType.SupportTeam:              return "Support team";

                case TeamType.ServersideTeam:               return "Serverside team";

                default: return teamType.ToString();
            }
        }

        public static int GetTeams(GameEntity company, TeamType teamType)
        {
            return company.team.Teams.Count(t => t.TeamType == teamType);
        }

        


        public static bool IsFullTeam(TeamInfo team)
        {
            int maxWorkers = 8;
            int workers = team.Workers; // Random.Range(0, maxWorkers);
            bool hasFullTeam = workers >= maxWorkers;

            return hasFullTeam;
        }


        public static bool IsNeverHiredEmployees(GameEntity product)
        {
            bool isFirstTeam = product.team.Teams.Count == 1;

            return isFirstTeam && product.team.Teams[0].Managers.Count <= 1;
        }

        public static bool IsTeamNeedsAttention(GameEntity product, TeamInfo team, GameContext gameContext)
        {
            bool hasNoManagerFocus = team.ManagerTasks.Contains(ManagerTask.None);


            bool hasNoManager = !HasMainManagerInTeam(team, gameContext, product);


            bool hasDisloyalManagers = team.Managers
                .Select(m => Humans.Get(gameContext, m))
                .Count(h => h.humanCompanyRelationship.Morale < 40 && Teams.GetLoyaltyChangeForManager(h, team, product) < 0) > 0;

            return IsFullTeam(team) && (hasNoManager || hasDisloyalManagers || IsTeamPromotable(product, team, gameContext));
        }
    }
}
