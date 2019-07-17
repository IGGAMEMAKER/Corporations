using Assets.Utils;
using UnityEngine;

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
        {
            Debug.Log("PromoteToGroupIfPossible " + company.company.Name + " " + company.isIndependentCompany);
            CompanyUtils.PromoteProductCompanyToGroup(gameContext, company.company.Id);
        }
    }
}
