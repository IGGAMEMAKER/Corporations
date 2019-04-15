using Entitas;
using UnityEngine;

public class ProductExecuteUpgradeEvent : IExecuteSystem
{
    readonly GameContext _context;

    public ProductExecuteUpgradeEvent(Contexts contexts)
    {
        _context = contexts.game;
    }

    GameEntity[] GetProductsWithUpgradeProductTask()
    {
        return _context.GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.EventUpgradeProduct, GameMatcher.Task));
    }

    public void Execute()
    {
        foreach (var e in GetProductsWithUpgradeProductTask())
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
        int explorationLevel = e.product.ExplorationLevel;
        int newLevel = e.eventUpgradeProduct.previousLevel + 1;

        if (explorationLevel < newLevel)
            explorationLevel = newLevel;

        e.ReplaceProduct(
            e.product.Id,
            e.product.Name,
            e.product.Niche,
            e.product.Industry,
            newLevel,
            explorationLevel,
            e.product.Resources
            );

        //Debug.Log($"upgraded product {e.product.Id}({e.product.Name}) to lvl {e.eventUpgradeProduct.previousLevel + 1}");
    }
}
