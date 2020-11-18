using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static int AddTeam(GameEntity company, TeamType teamType)
        {
            var prefix = GetFormattedTeamType(teamType);

            bool isCoreTeam = company.team.Teams.Count == 0;

            company.team.Teams.Add(new TeamInfo
            {
                Name = isCoreTeam ? "Core team" : $"{prefix} #{company.team.Teams.Count}",
                TeamType = teamType,
                Tasks = new List<TeamTask>(),

                //Offers = new System.Collections.Generic.Dictionary<int, JobOffer> { },
                Managers = new List<int>(),
                Roles = new Dictionary<int, WorkerRole>(),

                ManagerTasks = new List<ManagerTask> { ManagerTask.None, ManagerTask.None, ManagerTask.None },

                HiringProgress = 0,
                Workers = 0,
                Organisation = 0,
                ID = company.team.Teams.Count,

                TooManyLeaders = false
            });

            return company.team.Teams.Count - 1;
        }

        public static void RemoveTeam(GameEntity company, GameContext gameContext, int teamId)
        {
            var tasks = company.team.Teams[teamId].Tasks;
            var count = tasks.Count;

            while (tasks.Count > 0)
            {
                Teams.RemoveTeamTask(company, gameContext, teamId, 0);
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


        public static bool IsTeamNeedsAttention(GameEntity product, TeamInfo team, GameContext gameContext)
        {
            bool hasNoManagerFocus = team.ManagerTasks.Contains(ManagerTask.None);


            bool hasNoManager = !HasMainManagerInTeam(team, gameContext, product);


            bool hasDisloyalManagers = team.Managers
                .Select(m => Humans.Get(gameContext, m))
                .Count(h => h.humanCompanyRelationship.Morale < 40 && Teams.GetLoyaltyChangeForManager(h, team, product) < 0) > 0;

            return IsFullTeam(team) && (hasNoManager || hasNoManagerFocus || hasDisloyalManagers);
        }
    }
}
