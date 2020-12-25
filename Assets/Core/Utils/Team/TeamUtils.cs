using System;
using System.Collections;
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

                ManagerTasks = new List<ManagerTask> {ManagerTask.None, ManagerTask.None, ManagerTask.None},

                HiringProgress = 0,
                Workers = 0,
                Organisation = 0,
                ID = company.team.Teams.Count,
                Rank = TeamRank.Solo,

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

        public static bool IsCanReceiveTeams(TeamInfo team)
        {
            return team.isFullTeam && team.Rank == TeamRank.Department;
        }

        public static IEnumerable<TeamInfo> GetMergingCandidates(TeamInfo team, GameEntity company)
        {
            return company.team.Teams.Where(t => IsCanMergeTeams(team, t));
        }
        public static bool IsHasMergeCandidates(TeamInfo team, GameEntity company)
        {
            return GetMergingCandidates(team, company).Any();
        }
    
        public static bool IsCanMergeTeams(TeamInfo owner, TeamInfo t)
        {
            if (!IsCanReceiveTeams(owner))
                return false;
    
            return t.TeamType == owner.TeamType && !t.isCoreTeam && t.ID != owner.ID && t.ParentID != owner.ID;
        }
        
        public static void AttachTeamToTeam(TeamInfo dependant, TeamInfo owner)
        {
            dependant.ParentID = owner.ID;
        }

        public static IEnumerable<TeamInfo> GetDependantTeams(TeamInfo owner, GameEntity company)
        {
            return company.team.Teams.Where(t => t.ParentID == owner.ID);
        }

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
            bool hasLeadManager = HasMainManagerInTeam(team);

            var managerPoints = product.companyResource.Resources.managerPoints;
            var promotionCost = GetPromotionCost(team);

            var enoughManagementPoints = managerPoints >= promotionCost;

            return hasLeadManager && team.isFullTeam && enoughManagementPoints && team.Rank < TeamRank.Department;
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
            team.Rank = GetNextTeamRank(team.Rank);

            team.Name = GenerateTeamName(product, team);
            team.Organisation = Mathf.Min(team.Organisation, 10);

            var promotionCost = GetPromotionCost(team);
            Companies.SpendResources(product, new TeamResource(0, promotionCost, 0, 0, 0), "Team Promotion");
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
            WorkerRole managerTitle = GetMainManagerRole(teamInfo);

            var rating = GetEffectiveManagerRating(gameContext, product, managerTitle, teamInfo);

            var managerFocus = teamInfo.ManagerTasks.Count(t => t == ManagerTask.Organisation);

            bool NoCeo = rating < 1;

            // count team size: small teams organise faster, big ones: slower

            return new Bonus<int>("Organisation change")
                    .AppendAndHideIfZero("No " + managerTitle, NoCeo ? -30 : 0)
                    .AppendAndHideIfZero(managerTitle + " efforts", rating)
                    .AppendAndHideIfZero("Manager focus on Organisation", NoCeo ? 0 : managerFocus * 10)
                ;
        }

        public static bool IsUniversalTeam(TeamType teamType) =>
            new[] {TeamType.CrossfunctionalTeam}.Contains(teamType);

        public static string GetFormattedTeamType(TeamInfo team) =>
            GetFormattedTeamType(team.TeamType, team.Rank, team.isCoreTeam ? "Core team" : "");

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
                    return rank + " " + formattedName;
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
                .Count(h => h.humanCompanyRelationship.Morale < 40 &&
                            GetLoyaltyChangeForManager(h, team, product) < 0) > 0;

            return IsFullTeam(team) && (hasNoManager || hasDisloyalManagers || IsTeamPromotable(product, team));
        }
    }
}