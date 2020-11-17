using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static bool IsTaskSuitsTeam(TeamType teamType, TeamTask teamTask)
        {
            if (IsUniversalTeam(teamType))
            {
                if (teamTask.IsHighloadTask && (teamTask as TeamTaskSupportFeature).SupportFeature.SupportBonus.Max > 1_000_000)
                    return false;

                return true;
            }

            if (teamTask.IsFeatureUpgrade)
                return teamType == TeamType.DevelopmentTeam;

            if (teamTask.IsMarketingTask)
                return teamType == TeamType.MarketingTeam;

            if (teamTask.IsSupportTask)
                return teamType == TeamType.SupportTeam;

            if (teamTask.IsHighloadTask)
                return teamType == TeamType.ServersideTeam;

            return false;
        }

        public static void AddTeamTask(GameEntity product, GameContext gameContext, int teamId, TeamTask task)
        {
            var taskId = product.team.Teams[teamId].Tasks.Count;

            var slots = GetFreeSlotsForTaskType(product, task);

            if (!HasFreeSlotForTeamTask(product, task))
                task.IsPending = true;

            AddTeamTask(product, gameContext, teamId, taskId, task);
        }
        public static void AddTeamTask(GameEntity product, GameContext gameContext, int teamId, int taskId, TeamTask task)
        {
            var team = product.team.Teams[teamId];

            if (taskId >= team.Tasks.Count)
            {
                product.team.Teams[teamId].Tasks.Add(task);
            }
            else
            {
                try
                {
                    DisableTask(product, gameContext, teamId, taskId);
                    product.team.Teams[teamId].Tasks[taskId] = task;
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error on taskId: {taskId} / {product.team.Teams[teamId].Tasks.Count}" + taskId);
                }
            }

            ProcessTeamTaskIfNotPending(product, gameContext, task, team);
        }

        public static void ProcessTeamTaskIfNotPending(GameEntity product, GameContext gameContext, TeamTask task, TeamInfo team)
        {
            if (task.IsPending)
                return;

            if (task.IsFeatureUpgrade)
            {
                Products.UpgradeFeatureAndAddCooldown(product, (task as TeamTaskFeatureUpgrade).NewProductFeature.Name, gameContext);
            }

            if (task.IsMarketingTask)
            {
                var channel = Markets.GetMarketingChannel(gameContext, (task as TeamTaskChannelActivity).ChannelId);

                if (!Marketing.IsActiveInChannel(product, channel))
                    Marketing.EnableChannelActivity(product, gameContext, channel);
            }

            if (task.IsHighloadTask || task.IsSupportTask)
            {
                var name = (task as TeamTaskSupportFeature).SupportFeature.Name;

                if (!product.supportUpgrades.Upgrades.ContainsKey(name))
                {
                    product.supportUpgrades.Upgrades[name] = 0;
                }

                product.supportUpgrades.Upgrades[name]++;
            }
        }

        static void DisableTask(GameEntity product, GameContext gameContext, int teamId, int taskId) => DisableTask(product, gameContext, product.team.Teams[teamId].Tasks[taskId]);
        static void DisableTask(GameEntity product, GameContext gameContext, TeamTask task)
        {
            //Debug.Log($"Disabling task {task.ToString()} from {product.company.Name}");

            if (task.IsMarketingTask)
            {
                var activity = task as TeamTaskChannelActivity;

                var channel = Markets.GetAllMarketingChannels(gameContext).First(c => c.marketingChannel.ChannelInfo.ID == activity.ChannelId);
                Marketing.DisableChannelActivity(product, gameContext, channel);
            }

            if (task.IsFeatureUpgrade)
            {
                var activity = task as TeamTaskFeatureUpgrade;


                //var channel = Markets.GetMarketingChannels(gameContext).First(c => c.marketingChannel.ChannelInfo.ID == activity.ChannelId);
                //Products.DisableChannelActivity(product, gameContext, channel);
            }

            if (task.IsHighloadTask || task.IsSupportTask)
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

        public static void RemoveTeamTask(GameEntity product, GameContext gameContext, TeamTask task)
        {
            var slot = GetSlotOfTeamTask(product, task);

            if (slot == null)
                return;

            RemoveTeamTask(product, gameContext, slot.TeamId, slot.SlotId);
        }

        public static void RemoveTeamTask(GameEntity product, GameContext gameContext, int teamId, int taskId)
        {
            //Debug.Log($"Remove Task: {taskId} from team {teamId}");

            if (product.team.Teams[teamId].Tasks.Count() > taskId)
            {
                DisableTask(product, gameContext, teamId, taskId);

                product.team.Teams[teamId].Tasks.RemoveAt(taskId);
            }
        }
    }
}
