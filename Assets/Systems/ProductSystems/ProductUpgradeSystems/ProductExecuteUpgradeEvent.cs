using Assets.Utils;
using Entitas;

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

                NotificationUtils.AddNotification(_context, new NotificationLevelUp(e.company.Id, e.product.ProductLevel));
            }
        }
    }

    void UpgradeProduct(GameEntity e)
    {
        int explorationLevel = ProductDevelopmentUtils.GetMarketRequirementsInNiche(_context, e.product.Niche);
        int newLevel = e.eventUpgradeProduct.previousLevel + 1;

        if (explorationLevel < newLevel)
            explorationLevel = newLevel;

        e.ReplaceProduct(
            e.product.Id,
            e.product.Name,
            e.product.Niche,
            newLevel,
            e.product.ImprovementPoints + 1
            );
    }
}
