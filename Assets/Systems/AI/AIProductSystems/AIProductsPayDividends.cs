using Assets.Utils;
using System.Collections.Generic;

public partial class AIProductsPayDividends : OnMonthChange
{
    public AIProductsPayDividends(Contexts contexts) : base(contexts) {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in CompanyUtils.GetAIProducts(gameContext))
            PayDividendsIfPossible(e);
    }

    void PayDividendsIfPossible(GameEntity product)
    {
        if (product.isIndependentCompany)
            return;

        if (EconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, product))
            return;

        var maintenance = EconomyUtils.GetOptimalProductCompanyMaintenance(gameContext, product);
        long dividends = product.companyResource.Resources.money - maintenance;

        CompanyUtils.PayDividends(gameContext, product, dividends);
    }
}