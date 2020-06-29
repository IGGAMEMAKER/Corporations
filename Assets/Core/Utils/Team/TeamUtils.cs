using Assets.Core;
using System;
using System.Linq;
using UnityEngine;
// TODO REMOVE THIS FILE

namespace Assets.Core
{
    public static partial class Teams
    {
        private static void ReplaceTeam(GameEntity company, TeamComponent t)
        {
            company.ReplaceTeam(t.Morale, t.Organisation, t.Managers, t.Workers, t.Teams);
        }

        public static void ToggleCrunching(GameContext context, int companyId)
        {
            var c = Companies.Get(context, companyId);

            c.isCrunching = !c.isCrunching;
        }

        public static void SetRole(GameEntity company, int humanId, WorkerRole workerRole, GameContext gameContext)
        {
            var managers = company.team.Managers;

            managers[humanId] = workerRole;

            company.ReplaceTeam(company.team.Morale, company.team.Organisation, managers, company.team.Workers, company.team.Teams);

            Humans.SetRole(gameContext, humanId, workerRole);
        }

        public static string GetFormattedTeamType(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.BigCrossfunctionalTeam:   return "Big team";
                case TeamType.CrossfunctionalTeam:      return "Universal team";
                case TeamType.SmallCrossfunctionalTeam: return "Small team";

                case TeamType.CoreTeam:                 return "Core team";
                case TeamType.DevelopmentTeam:          return "Development team";
                case TeamType.MarketingTeam:            return "Marketing team";
                case TeamType.MergeAndAcquisitionTeam:  return "M&A team";
                case TeamType.SupportTeam:              return "Support team";

                case TeamType.DevOpsTeam:               return "Serverside team";

                default: return teamType.ToString();
            }
        }

        public static void AddTeam(GameEntity company, TeamType teamType)
        {
            var prefix = GetFormattedTeamType(teamType);

            company.team.Teams.Add(new TeamInfo { Name = $"{prefix} #{company.team.Teams.Count}", TeamType = teamType, Tasks = new System.Collections.Generic.List<TeamTask>() });
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
            Debug.Log("Team removed!");
        }

        public static int GetAmountOfWorkersByTeamType(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.MarketingTeam: return 3;
                case TeamType.DevelopmentTeam: return 3;
                case TeamType.SmallCrossfunctionalTeam: return 3;
                case TeamType.CrossfunctionalTeam: return 10;
                case TeamType.BigCrossfunctionalTeam: return 20;

                default: return 1000;
            }
        }

        public static int GetAmountOfTeams(GameEntity company, TeamType teamType)
        {
            return company.team.Teams.FindAll(t => t.TeamType == teamType).Count;
        }

        public static int GetAmountOfPossibleChannelsByTeamType(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.BigCrossfunctionalTeam: return 3;
                case TeamType.CrossfunctionalTeam: return 2;
                case TeamType.SmallCrossfunctionalTeam: return 1;
                case TeamType.MarketingTeam: return 2;

                default: return 0;
            }
        }

        public static int GetAmountOfPossibleFeaturesByTeamType(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.BigCrossfunctionalTeam: return 3;
                case TeamType.CrossfunctionalTeam: return 2;
                case TeamType.SmallCrossfunctionalTeam: return 1;
                case TeamType.DevelopmentTeam: return 2;

                default: return 0;
            }
        }

        public static void AddTeamTask(GameEntity product, GameContext gameContext, int teamId, TeamTask task)
        {
            var taskId = product.team.Teams[teamId].Tasks.Count;

            AddTeamTask(product, gameContext, teamId, taskId, task);
        }
        public static void AddTeamTask(GameEntity product, GameContext gameContext, int teamId, int taskId, TeamTask task)
        {
            if (taskId >= product.team.Teams[teamId].Tasks.Count)
                product.team.Teams[teamId].Tasks.Add(task);
            else
            {
                try
                {
                    DisableTask(product, gameContext, teamId, taskId);
                    product.team.Teams[teamId].Tasks[taskId] = task;
                }
                catch (Exception ex)
                {
                    Debug.Log($"Error on taskId: {taskId} / {product.team.Teams[teamId].Tasks.Count}" + taskId);
                }
            }
        }

        static void DisableTask(GameEntity product, GameContext gameContext, int teamId, int taskId)
        {
            var task = product.team.Teams[teamId].Tasks[taskId];
            Debug.Log($"Disabling task from {product.company.Name} slotId={taskId} ");

            if (task.IsMarketingTask())
            {
                var activity = task as TeamTaskChannelActivity;

                var channel = Markets.GetMarketingChannels(gameContext).First(c => c.marketingChannel.ChannelInfo.ID == activity.ChannelId);
                Marketing.DisableChannelActivity(product, gameContext, channel);
            }

            if (task.IsFeatureUpgrade())
            {
                var activity = task as TeamTaskFeatureUpgrade;

                //var channel = Markets.GetMarketingChannels(gameContext).First(c => c.marketingChannel.ChannelInfo.ID == activity.ChannelId);
                //Products.DisableChannelActivity(product, gameContext, channel);
            }

            if (task is TeamTaskSupportFeature)
            {
                var activity = task as TeamTaskSupportFeature;

                var up = product.supportUpgrades.Upgrades;

                var name = activity.SupportFeature.Name;
                if (up.ContainsKey(name))
                {
                    up[name]--;
                }

                if (up[name] <= 0)
                {
                    up.Remove(name);
                }
            }
        }

        public static void RemoveTeamTask(GameEntity product, GameContext gameContext, int teamId, int taskId)
        {
            Debug.Log($"Remove Task: {taskId} from team {teamId}");

            DisableTask(product, gameContext, teamId, taskId);

            product.team.Teams[teamId].Tasks.RemoveAt(taskId);
        }
    }
}
