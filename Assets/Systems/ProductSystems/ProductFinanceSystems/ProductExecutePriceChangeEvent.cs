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
        int newPrice = e.eventFinancePricingChange.level + e.eventFinancePricingChange.change;

        if (newPrice < 0) newPrice = 0;
        if (newPrice > 11) newPrice = 11;

        e.ReplaceFinance(newPrice, e.finance.marketingFinancing, e.finance.salaries, e.finance.basePrice);

        //Debug.Log($"ChangePrice product {e.product.Id}({e.product.Name}) from {e.eventFinancePricingChange.level} by {e.eventFinancePricingChange.change} to {newPrice}");
    }
}
