using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void GrowCompanyCost(GameEntity company)
    {
        ManageProductCompany(company);

        PromoteToGroupIfPossible(company);
    }

    void Operate(GameEntity company)
    {
        ManageProductCompany(company);

        if (company.isIndependentCompany)
            PromoteToGroupIfPossible(company);
    }

    void PromoteToGroupIfPossible(GameEntity company)
    {
        if (CompanyEconomyUtils.GetBalanceChange(company, gameContext) > 1000000)
            CompanyUtils.PromoteProductCompanyToGroup(gameContext, company.company.Id);
    }
}
