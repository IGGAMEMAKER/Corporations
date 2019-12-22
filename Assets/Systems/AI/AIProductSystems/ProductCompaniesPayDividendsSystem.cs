using Assets.Utils;
using System.Collections.Generic;

public partial class ProductCompaniesPayDividendsSystem : OnPeriodChange
{
    public ProductCompaniesPayDividendsSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var dependantProducts = Companies.GetDependentProducts(gameContext);

        foreach (var e in dependantProducts)
            PayDividends(e);
    }

    void PayDividends(GameEntity e)
    {
        var dividends = Companies.BalanceOf(e);
        if (Companies.IsCompanyRelatedToPlayer(gameContext, e))
            Companies.PayDividends(gameContext, e, dividends);
        else
        {
            dividends -= Economy.GetCompanyMaintenance(gameContext, e);

            if (dividends > 0)
                Companies.PayDividends(gameContext, e, dividends);
        }
    }
}