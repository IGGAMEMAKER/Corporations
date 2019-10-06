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

        if (CompanyUtils.IsCompanyRelatedToPlayer(gameContext, product))
        {
            CompanyUtils.PayDividends(gameContext, product, EconomyUtils.GetBalanceChange(product, gameContext));
            return;
        }


        if (EconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, product))
            return;

        var maintenance = EconomyUtils.GetOptimalProductCompanyMaintenance(gameContext, product);
        long dividends = product.companyResource.Resources.money - maintenance;

        CompanyUtils.PayDividends(gameContext, product, dividends);
    }
}