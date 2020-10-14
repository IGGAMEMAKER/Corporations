using Assets.Core;
using System.Collections.Generic;

public partial class ProductCompaniesPayDividendsSystem : OnPeriodChange
{
    public ProductCompaniesPayDividendsSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var dependantProducts = Companies.GetDependentProducts(gameContext);

        foreach (var e in dependantProducts)
        {
            if (Companies.IsPlayerFlagship(e))
                PayPlayerDividends(e);
            else
                PayAIDividends(e);
        }
    }

    void PayPlayerDividends(GameEntity e)
    {
        var dividends = Economy.BalanceOf(e);

        Companies.PayDividends(gameContext, e, dividends);
    }

    void PayAIDividends(GameEntity e)
    {
        // TODO pay dividends only if all upgrades are set to max and still they have necessary amount of money

        var balance = Economy.BalanceOf(e);
        var maintenance = Economy.GetProductMaintenance(e, gameContext);

        var dividends = balance - maintenance;
        
        if (dividends > 0)
            Companies.PayDividends(gameContext, e, dividends);
    }
}