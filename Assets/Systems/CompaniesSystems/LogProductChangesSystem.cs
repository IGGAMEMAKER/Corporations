using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class LogProductChangesSystem : ReactiveSystem<GameEntity>
{
    private Contexts contexts;

    public LogProductChangesSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
            Debug.Log($"Our {entity.product.Name} changed!");
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasProduct && entity.isControlledByPlayer;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Product);
    }
}

public class ProductUpgradeEventHandler : ReactiveSystem<GameEntity>
{
    private Contexts contexts;
    IGroup<GameEntity> products;

    public ProductUpgradeEventHandler(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
        products = contexts.game.GetGroup(GameMatcher.Product);
    }

    GameEntity GetProductById(int id)
    {
        //GameEntity[] entities = contexts.game.GetEntities(GameMatcher.Product);
        

        foreach (var e in products)
        {
            if (e.product.Id == id)
                return e;
        }

        return null;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Debug.Log($"found {entities.Count} ProductUpgradeEvent");
        foreach (var e in entities)
        {
            var eventUpgradeProductComponent = e.eventUpgradeProduct;
            GameEntity p = GetProductById(eventUpgradeProductComponent.productId);

            if (p != null)
            {
                p.ReplaceProduct(
                    eventUpgradeProductComponent.productId,
                    p.product.Name,
                    p.product.Niche,
                    eventUpgradeProductComponent.previousLevel + 1,
                    p.product.ExplorationLevel,
                    p.product.Resources
                    );

                Debug.Log($"upgraded product {eventUpgradeProductComponent.productId}" +
                    $" to lvl {eventUpgradeProductComponent.previousLevel + 1}");
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasEventUpgradeProduct;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.EventUpgradeProduct);
    }
}
