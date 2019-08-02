using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void ManageProductCompany(GameEntity company)
    {
        //Debug.Log("Manage Product Company ")

        ManageProductTeam(company);

        ManageProductDevelopment(company);

        ManageInvestors(company);
    }

    void ManageProductDevelopment(GameEntity company)
    {
        UpgradeSegment(company);
    }

    void ManageInvestors(GameEntity company)
    {
        // taking investments
        TakeInvestments(company);
    }

    void PayDividends(GameEntity product)
    {
        if (CompanyEconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, product))
            return;

        if (product.isIndependentCompany)
            return;

        var dividends = product.companyResource.Resources.money - CompanyEconomyUtils.GetCompanyMaintenance(gameContext, product.company.Id);

        CompanyUtils.PayDividends(gameContext, product, dividends);
    }
}
