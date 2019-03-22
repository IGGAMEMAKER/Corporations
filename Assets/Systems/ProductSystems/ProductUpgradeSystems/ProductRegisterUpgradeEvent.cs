using System.Collections.Generic;
using Assets.Utils;
using Entitas;

public class ProductRegisterUpgradeEvent : ReactiveSystem<GameEntity>
{
    private readonly Contexts contexts;

    public ProductRegisterUpgradeEvent(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
    }

    void AddTask(GameEntity gameEventEntity)
    {
        int time = ProductDevelopmentUtils.GetIterationTime(gameEventEntity);

        TaskComponent task = ScheduleUtils.GenerateTaskComponent(contexts.game, TaskType.UpgradeProduct, time);

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
