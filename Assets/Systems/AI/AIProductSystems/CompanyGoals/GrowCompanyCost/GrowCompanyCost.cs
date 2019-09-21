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
        if (company.isIndependentCompany)
        {
            var profit = CompanyEconomyUtils.GetBalanceChange(company, gameContext);
            var canGrow = profit > 1000000;

            var ambitions = HumanUtils.GetFounderAmbition(gameContext, company.cEO.HumanId);
            var wantsToGrow = ambitions != Ambition.RuleProductCompany;
            
            if (canGrow && wantsToGrow)
                CompanyUtils.PromoteProductCompanyToGroup(gameContext, company.company.Id);
        }
    }
}
