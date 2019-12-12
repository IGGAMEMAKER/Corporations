using Assets.Utils;
using System.Collections.Generic;

public partial class ProductCompaniesPayDividendsSystem : OnPeriodChange
{
    public ProductCompaniesPayDividendsSystem(Contexts contexts) : base(contexts) {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in Companies.GetProductCompanies(gameContext))
            PayDividendsIfPossible(e);
    }

    void PayDividendsIfPossible(GameEntity product)
    {
        if (product.isIndependentCompany)
            return;


        if (Companies.IsCompanyRelatedToPlayer(gameContext, product))
        {
            var profit = Economy.GetProfit(product, gameContext);
            Companies.PayDividends(gameContext, product, profit);

            return;
        }

        if (Economy.IsCompanyNeedsMoreMoneyOnMarket(gameContext, product))
            return;



        long dividends = product.companyResource.Resources.money;
        Companies.PayDividends(gameContext, product, dividends);
    }
}