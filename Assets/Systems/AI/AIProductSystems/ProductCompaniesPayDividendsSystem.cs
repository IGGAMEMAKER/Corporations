using Assets.Utils;
using System.Collections.Generic;

public partial class ProductCompaniesPayDividendsSystem : OnMonthChange
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

        var balanceChange = EconomyUtils.GetProfit(product, gameContext);

        if (CompanyUtils.IsCompanyRelatedToPlayer(gameContext, product))
        {
            CompanyUtils.PayDividends(gameContext, product, balanceChange);
            return;
        }


        if (EconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, product))
            return;

        //if (balanceChange)
        long dividends = product.companyResource.Resources.money;

        CompanyUtils.PayDividends(gameContext, product, dividends);
    }
}