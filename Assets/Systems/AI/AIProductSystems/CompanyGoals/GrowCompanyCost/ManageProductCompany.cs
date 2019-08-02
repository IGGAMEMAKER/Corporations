using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void ManageProductCompany(GameEntity product)
    {
        //Debug.Log("Manage Product Company ")

        ManageProductTeam(product);

        ManageProductDevelopment(product);

        ManageInvestors(product);

        PayDividendsIfPossible(product);
    }

    void ManageProductDevelopment(GameEntity product)
    {
        UpgradeSegment(product);
    }

    void ManageInvestors(GameEntity product)
    {
        // taking investments
        TakeInvestments(product);
    }

    void PayDividendsIfPossible(GameEntity product)
    {
        if (CompanyEconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, product))
            return;

        if (product.isIndependentCompany)
            return;

        var maintenance = CompanyEconomyUtils.GetOptimalProductCompanyMaintenance(gameContext, product);
        var dividends = product.companyResource.Resources.money - maintenance;

        CompanyUtils.PayDividends(gameContext, product, dividends);
    }
}
