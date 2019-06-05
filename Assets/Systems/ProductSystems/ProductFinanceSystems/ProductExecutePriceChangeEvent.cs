using Assets.Utils;
using Entitas;

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
            ChangePrice(e);
            e.RemoveEventFinancePricingChange();
        }
    }

    void ChangePrice(GameEntity e)
    {
        var newPrice = e.eventFinancePricingChange.level;

        CompanyUtils.SetPrice(e, newPrice);
    }
}
