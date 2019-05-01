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
        return _context.GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.EventUpgradeProduct));
    }

    public void Execute()
    {
        foreach (var e in GetProductsWithUpgradeProductTask())
        {
            UpgradeProduct(e);
            e.RemoveEventUpgradeProduct();
        }
    }

    void UpgradeProduct(GameEntity e)
    {
        int explorationLevel = NicheUtils.GetLeaderApp(_context, e.company.Id).product.ProductLevel;

        int newLevel = e.eventUpgradeProduct.previousLevel + 1;

        if (explorationLevel < newLevel)
        {
            explorationLevel = newLevel;

            NotificationUtils.AddNotification(_context, new NotificationLevelUp(e.company.Id, e.product.ProductLevel));
        }

        e.ReplaceProduct(
            e.product.Id,
            e.product.Name,
            e.product.Niche,
            newLevel,
            e.product.ImprovementPoints,
            e.product.Segments
            );
    }
}
