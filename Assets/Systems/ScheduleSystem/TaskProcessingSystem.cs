using System.Collections.Generic;
using Assets.Core;
using Entitas;
using UnityEngine;
using System;

public partial class TaskProcessingSystem : OnDateChange
{
    public TaskProcessingSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var date = ScheduleUtils.GetCurrentDate(gameContext);

        // TIMED ACTIONS
        // TODO unnecessary?
        ProcessTimedActions(date);

        // PROCESS TEAM TASKS
        ProcessTeamTasks(date);
    }

    void ProcessTeamTasks(int date)
    {
        var products = Companies.GetProductCompanies(gameContext);

        foreach (var p in products)
        {
            List<SlotInfo> removableTasks = new List<SlotInfo>();

            int teamId = 0;
            foreach (var team in p.team.Teams)
            {
                int slotId = 0;

                foreach (var task in team.Tasks)
                {
                    if (task.IsPending && Teams.HasFreeSlotForTeamTask(p, task) && Teams.CanExecuteTeamTask(p, task))
                    {
                        task.IsPending = false;

                        Teams.InitializeTeamTaskIfNotPending(p, date, gameContext, task);
                    }

                    Teams.ProcessTeamTaskIfNotPending(p, date, task, ref removableTasks, slotId, teamId);

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

    void ProcessTimedActions(int date)
    {
        GameEntity[] tasks = Cooldowns.GetTimedActions(gameContext);
        
        for (var i = tasks.Length - 1; i >= 0; i--)
        {
            try
            {
                var t = tasks[i];
                var task = t.timedAction;

                var endTime = task.EndTime;

                if (t.isTask)
                {
                    if (date >= endTime && !task.isCompleted)
                    {
                        Cooldowns.ProcessTask(task, gameContext);
                        t.timedAction.isCompleted = true;
                    }
                }


                // 
                if (date > endTime)
                    t.Destroy();
            }
            catch (Exception ex)
            {
                Debug.Log("Error in task processing system");
                Debug.Log(ex);
            }
        }
    }

    protected override bool Filter(GameEntity entity) => entity.hasDate;

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) => context.CreateCollector(GameMatcher.Date);
}
