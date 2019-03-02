using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ProductUpgradeEventHandler : ReactiveSystem<GameEntity>
{
    private Contexts contexts;
    IGroup<GameEntity> products;
    IGroup<GameEntity> UpgradeProductEvents;

    public ProductUpgradeEventHandler(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
        products = contexts.game.GetGroup(GameMatcher.Product);
        UpgradeProductEvents = contexts.game.GetGroup(GameMatcher.EventUpgradeProduct);
    }

    void UpgradeProduct (GameEntity e) {
        e.ReplaceProduct(
            e.product.Id,
            e.product.Name,
            e.product.Niche,
            e.eventUpgradeProduct.previousLevel + 1,
            e.product.ExplorationLevel,
            e.product.Resources
            );

        Debug.Log($"upgraded product {e.eventUpgradeProduct.productId}({e.product.Name})" +
            $" to lvl {e.eventUpgradeProduct.previousLevel + 1}");
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
        TaskComponent task = GenerateTaskComponent(TaskType.UpgradeProduct, 10);

        gameEventEntity.AddTask(task.isCompleted, task.TaskType, task.StartTime, task.Duration, task.EndTime);
    }

    protected override void Execute(List<GameEntity> entities)
    {
        //Debug.Log($"found {entities.Count}/{UpgradeProductEvents.count} ProductUpgradeEvent");
        foreach (var e in entities)
        {
            if (!e.hasTask)
                AddTask(e);
            else if (e.task.isCompleted)
            {
                UpgradeProduct(e);
                e.RemoveEventUpgradeProduct();
                e.RemoveTask();
                // Cleanup
                //RemoveTask()...
                //RemoveUpgradeEvent
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasEventUpgradeProduct && entity.hasProduct;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.EventUpgradeProduct);
    }
}
