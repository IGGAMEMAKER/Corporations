using Entitas;
using UnityEngine;

public class ProductExecutePriceChangeEvent : IExecuteSystem
{
    readonly GameContext _context;

    public ProductExecutePriceChangeEvent(Contexts contexts)
    {
        _context = contexts.game;
    }

    GameEntity[] GetProductsWithPriceChangeEvent()
    {
        return _context.GetEntities(GameMatcher.AllOf(GameMatcher.Finance, GameMatcher.EventFinancePricingChange));
    }

    public void Execute()
    {
        foreach (var e in GetProductsWithPriceChangeEvent())
        {
            UpgradeProduct(e);
            e.RemoveEventUpgradeProduct();
            e.RemoveTask();
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

        Debug.Log($"upgraded product {e.product.Id}({e.product.Name}) to lvl {e.eventUpgradeProduct.previousLevel + 1}");
    }
}
