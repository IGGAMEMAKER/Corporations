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

    GameEntity GetProductById(int id)
    {
        foreach (var e in products)
        {
            if (e.product.Id == id)
                return e;
        }

        return null;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Debug.Log($"found {entities.Count}/{UpgradeProductEvents.count} ProductUpgradeEvent");
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

                Debug.Log($"upgraded product {eventUpgradeProductComponent.productId}({p.product.Name})" +
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
