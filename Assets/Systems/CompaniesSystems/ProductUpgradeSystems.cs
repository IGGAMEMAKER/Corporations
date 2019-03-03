using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ProductUpgradeSystems : Feature
{
    public ProductUpgradeSystems(Contexts contexts) : base("Product Upgade Systems")
    {
        // Adds task when upgrade product button is pressed
        Add(new ProductUpgradeEventHandler(contexts));

        // updates product data
        Add(new ProductProcessUpgradeEvent(contexts));
    }
}

public class ProductProcessUpgradeEvent : IExecuteSystem
{
    readonly GameContext _context;

    public ProductProcessUpgradeEvent(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Execute()
    {
        foreach (var e in _context.GetEntities(GameMatcher.EventUpgradeProduct))
        {
            if (e.task.isCompleted)
            {
                UpgradeProduct(e);
                e.RemoveEventUpgradeProduct();
                e.RemoveTask();
            }
        }
    }

    void UpgradeProduct(GameEntity e)
    {
        e.ReplaceProduct(
            e.product.Id,
            e.product.Name,
            e.product.Niche,
            e.eventUpgradeProduct.previousLevel + 1,
            e.product.ExplorationLevel,
            e.product.Resources
            );

        Debug.Log($"upgraded product {e.product.Id}({e.product.Name}) to lvl {e.eventUpgradeProduct.previousLevel + 1}");
    }
}

public class ProductUpgradeEventHandler : ReactiveSystem<GameEntity>
{
    private readonly Contexts contexts;

    public ProductUpgradeEventHandler(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
    }

    TaskComponent GenerateTaskComponent(TaskType taskType, int duration)
    {
        int currentDate = Contexts.sharedInstance.game.GetEntities(GameMatcher.Date)[0].date.Date;

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
