using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void Operate(GameEntity company)
    {
        ManageProductCompany(company);

        PromoteToGroupIfPossible(company);
    }

    void PromoteToGroupIfPossible(GameEntity company)
    {
        if (company.isIndependentCompany && CompanyEconomyUtils.GetBalanceChange(company, gameContext) > 1000000)
            CompanyUtils.PromoteProductCompanyToGroup(gameContext, company.company.Id);
    }
}
