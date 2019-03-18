using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ScheduleTaskProcessingSystem : ReactiveSystem<GameEntity>
{
    private Contexts contexts;

    public ScheduleTaskProcessingSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
    }

    void ProcessTasks(DateComponent date)
    {
        GameEntity[] tasks = contexts.game.GetEntities(GameMatcher.Task);

        if (tasks.Length > 0)
            Debug.Log("Process tasks: " + tasks.Length);

        //Debug.Log("date: " + date.Date);

        foreach (var t in tasks)
        {
            //Debug.Log("Task t: " + t.task.EndTime);

            if (date.Date >= t.task.EndTime)
            {
                t.ReplaceTask(true, t.task.TaskType, t.task.StartTime, t.task.Duration, t.task.EndTime);
                Debug.Log("Complete task");
                // TODO: play sounds for some taskTypes maybe??
            }
        }
    }

    protected override void Execute(List<GameEntity> entities)
    {
        ProcessTasks(entities[0].date);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDate;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Date);
    }
}
