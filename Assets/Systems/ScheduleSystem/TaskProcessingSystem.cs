using System.Collections.Generic;
using Assets.Core;
using Entitas;
using System.Linq;
using UnityEngine;
using System;

public partial class TaskProcessingSystem : OnDateChange
{
    public TaskProcessingSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var date = ScheduleUtils.GetCurrentDate(gameContext);

        //Debug.Log("Loaded date: " + date);

        GameEntity[] tasks = Cooldowns.GetTimedActions(gameContext);

        //Debug.Log($"Loaded {tasks.Count()} Tasks");

        for (var i = tasks.Length - 1; i >= 0; i--)
        {
            try
            {
                var t = tasks[i];
                var task = t.timedAction;

                var EndTime = task.EndTime;

                if (t.isTask)
                {
                    if (date >= EndTime && !task.isCompleted)
                    {
                        Cooldowns.ProcessTask(task, gameContext);
                        t.timedAction.isCompleted = true;
                    }
                }


                // 
                if (date > EndTime)
                    t.Destroy();
            }
            catch (Exception ex)
            {
                Debug.Log("Error in task processing system");
                Debug.Log(ex);
            }
        }

        //return;
        var products = Companies.GetProductCompanies(gameContext);

        Debug.Log("Products " + products.Count());

        foreach (var p in products)
        {
            List<SlotInfo> removableTasks = new List<SlotInfo>();

            int teamId = 0;
            foreach (var team in p.team.Teams)
            {
                int slotId = 0;
                foreach (var task in team.Tasks)
                {
                    if (task is TeamTaskFeatureUpgrade)
                    {
                        var upgrade = (task as TeamTaskFeatureUpgrade);

                        var featureName = upgrade.NewProductFeature.Name;

                        if (!Products.IsUpgradingFeature(p, gameContext, featureName))
                        {
                            Products.UpgradeFeature(p, featureName, gameContext, team);

                            // -----------------------

                            var cap = Products.GetFeatureRatingCap(p, gameContext);

                            if (p.features.Upgrades[featureName] >= cap)
                            {
                                removableTasks.Add(new SlotInfo { SlotId = slotId, TeamId = teamId });
                            }
                        }
                    }

                    if (task is TeamTaskChannelActivity)
                    {
                        // channel tookout
                    }

                    slotId++;
                }

                teamId++;
            }

            // remove expired tasks
            removableTasks.Reverse();
            foreach (var s in removableTasks)
            {
                Teams.RemoveTeamTask(p, gameContext, s.TeamId, s.SlotId);
            }
        }
    }




    protected override bool Filter(GameEntity entity) => entity.hasDate;

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) => context.CreateCollector(GameMatcher.Date);
}
