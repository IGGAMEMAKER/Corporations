using Assets.Utils;
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

    void UpdateTechLeader(GameEntity e, int level, GameEntity nicheStateEntity)
    {
        e.isTechnologyLeader = true;

        nicheStateEntity.ReplaceNicheState(nicheStateEntity.nicheState.Growth, level);

        NotificationUtils.AddNotification(_context, new NotificationLevelUp(e.company.Id, e.product.ProductLevel));
    }

    void RemoveTechLeaders(GameEntity e)
    {
        var niche = e.product.Niche;

        var competitors = NicheUtils.GetPlayersOnMarket(_context, niche);

        foreach (var c in competitors)
            c.isTechnologyLeader = false;
    }

    void UpgradeProduct(GameEntity e)
    {
        Debug.Log("Upgrade product " + e.company.Name);

        var niche = NicheUtils.GetNicheEntity(_context, e.product.Niche);

        int explorationLevel = niche.nicheState.Level;

        int newLevel = e.eventUpgradeProduct.previousLevel + 1;

        if (newLevel > explorationLevel)
            UpdateTechLeader(e, newLevel, niche);
        else if (newLevel == explorationLevel)
            RemoveTechLeaders(e);

        e.ReplaceProduct(
            e.product.Id,
            e.product.Name,
            e.product.Niche,
            newLevel,
            e.product.Segments
            );
    }
}
