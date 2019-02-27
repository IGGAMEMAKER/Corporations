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
        {
            Debug.Log($"Our {entity.product.Name}.ProductLevel updated to {entity.product.ProductLevel}");
        }
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

    public ProductUpgradeEventHandler(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
    }

    GameEntity GetProductById(int id)
    {
        GameEntity[] entities = contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.Product));
        Debug.Log($"Searching entity by productId. Found {entities.Length} possible candidates");

        foreach (var e in entities)
        {
            if (e.product.Id == id)
            {
                Debug.Log($"found entity with productComponent.id={id}");

                
                return e;
            }
        }

        Debug.Log("Entities not found in GetProductById");
        return null;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Debug.Log($"found {entities.Count} ProductUpgradeEvent");
        foreach (var e in entities)
        {
            var ev = e.eventUpgradeProduct;
            Debug.Log($"trying to update {ev.productId}");

            GameEntity p = GetProductById(ev.productId);
            ProductComponent product = p.product;

            Debug.Log($"Found entity with product: {p.ToString()}");
            Debug.Log($"Found product with fields: {product.ToString()}");

            p.ReplaceProduct(
                ev.productId,
                product.Name,
                product.Niche,
                ev.previousLevel + 1,
                product.ExplorationLevel,
                product.Resources
                );
            Debug.Log($"upgraded product {ev.productId} to lvl {ev.previousLevel + 1}");
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
