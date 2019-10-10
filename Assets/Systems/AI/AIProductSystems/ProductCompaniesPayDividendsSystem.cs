using Assets.Utils;
using System.Collections.Generic;

public partial class ProductCompaniesPayDividendsSystem : OnPeriodChange
{
    public ProductCompaniesPayDividendsSystem(Contexts contexts) : base(contexts) {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in CompanyUtils.GetProductCompanies(gameContext))
            PayDividendsIfPossible(e);
    }

    void PayDividendsIfPossible(GameEntity product)
    {
        if (product.isIndependentCompany)
            return;


        if (CompanyUtils.IsCompanyRelatedToPlayer(gameContext, product))
        {
            var profit = EconomyUtils.GetProfit(product, gameContext);
            CompanyUtils.PayDividends(gameContext, product, profit);

            return;
        }

        if (EconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, product))
            return;



        long dividends = product.companyResource.Resources.money;
        CompanyUtils.PayDividends(gameContext, product, dividends);
    }
}