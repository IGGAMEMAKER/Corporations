using Assets.Utils;
using System.Collections.Generic;

public partial class ProductCompaniesPayDividendsSystem : OnPeriodChange
{
    public ProductCompaniesPayDividendsSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var dependantProducts = Companies.GetDependentProducts(gameContext);

        foreach (var e in dependantProducts)
        {
            if (Companies.IsCompanyRelatedToPlayer(gameContext, e))
                PayPlayerDividends(e);
            else
                PayAIDividends(e);
        }
    }

    void PayPlayerDividends(GameEntity e)
    {
        var dividends = Companies.BalanceOf(e);

        Companies.PayDividends(gameContext, e, dividends);
    }

    void PayAIDividends(GameEntity e)
    {
        var dividends = Companies.BalanceOf(e) - Economy.GetCompanyMaintenance(gameContext, e);

        Companies.PayDividends(gameContext, e, dividends);
    }
}