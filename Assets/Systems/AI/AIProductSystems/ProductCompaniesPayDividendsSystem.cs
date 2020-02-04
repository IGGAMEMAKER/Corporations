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
            if (Companies.IsRelatedToPlayer(gameContext, e) && Companies.IsPlayerFlagship(gameContext, e))
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
        long marketingBudget = 0;

        if (e.hasProduct)
            marketingBudget = Marketing.GetBrandingCost(e, gameContext) + Marketing.GetTargetingCost(e, gameContext);


        var dividends = Economy.BalanceOf(e) - Economy.GetCompanyMaintenance(gameContext, e) - marketingBudget;
        
        if (dividends > 0)
            Companies.PayDividends(gameContext, e, dividends);
    }
}