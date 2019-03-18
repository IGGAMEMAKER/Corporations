using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ProductUpgradeHandlerSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts contexts;

    public ProductUpgradeHandlerSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
    }

    TaskComponent GenerateTaskComponent(TaskType taskType, int duration)
    {
        int currentDate = contexts.game.GetEntities(GameMatcher.Date)[0].date.Date;

        return new TaskComponent
        {
            Duration = duration,
            isCompleted = false,
            TaskType = taskType,
            StartTime = currentDate,
            EndTime = currentDate + duration
        };
    }

    void AddTask(GameEntity gameEventEntity)
    {
        TaskComponent task = GenerateTaskComponent(TaskType.UpgradeProduct, 5);

        gameEventEntity.AddTask(task.isCompleted, task.TaskType, task.StartTime, task.Duration, task.EndTime);
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
            AddTask(e);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasEventUpgradeProduct && entity.hasProduct && !entity.hasTask;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.EventUpgradeProduct);
    }
}
