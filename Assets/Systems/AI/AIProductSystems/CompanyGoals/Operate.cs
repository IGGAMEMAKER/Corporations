using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void Operate(GameEntity company)
    {
        ManageProductCompany(company);

        PayDividendsIfPossible(company);

        PromoteToGroupIfPossible(company);

        //InvestmentUtils.CompleteGoal(e, gameContext, false);
    }

    void PromoteToGroupIfPossible(GameEntity company)
    {
        if (!company.isIndependentCompany)
            return;

        var profit = CompanyEconomyUtils.GetBalanceChange(company, gameContext);
        var canGrow = profit > 1000000;

        var ambitions = HumanUtils.GetFounderAmbition(gameContext, company.cEO.HumanId);
        var wantsToGrow = ambitions != Ambition.RuleProductCompany;
            
        if (canGrow && wantsToGrow)
            CompanyUtils.PromoteProductCompanyToGroup(gameContext, company.company.Id);
    }

    void PayDividendsIfPossible(GameEntity product)
    {
        if (CompanyEconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, product))
            return;

        if (product.isIndependentCompany || product.isAggressiveMarketing)
            return;

        var maintenance = CompanyEconomyUtils.GetOptimalProductCompanyMaintenance(gameContext, product);
        var dividends = product.companyResource.Resources.money - maintenance;

        CompanyUtils.PayDividends(gameContext, product, dividends);
    }
}
