using System.Collections.Generic;
using System.Linq;
using Assets.Core;
using UnityEngine;

public partial class CheckBankruptciesSystem : OnPeriodChange
{
    public CheckBankruptciesSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var bankruptCompanies = Companies.GetIndependentCompanies(gameContext)
            .Where(Companies.IsNotFinancialStructure)
            .Where(IsBankrupt);

        foreach (var c in bankruptCompanies)
            CloseBankruptCompanies(c);
    }

    bool IsBankrupt(GameEntity company) => Economy.BalanceOf(company) < 0;

    void CloseBankruptCompanies(GameEntity company)
    {
        Debug.Log("Bankrupt: " + company.company.Name);

        if (company.hasProduct)
        {
            var maintenance = Economy.GetProductCompanyMaintenance(company, gameContext, true);
            var income = Economy.GetProductCompanyIncome(company, gameContext);
            var 
            Debug.Log($"Economy: <color=green>Income</color> - {Format.MinifyMoney(income)}\n<color=red>Maintenance</color> - {maintenance.MinifyValues().ToString(true)}");
        }

        if (Companies.IsPlayerCompany(gameContext, company))
            NotificationUtils.AddPopup(gameContext, new PopupMessageGameOver(company.company.Id));

        Companies.CloseCompany(gameContext, company);
        ScheduleUtils.TweakCampaignStats(gameContext, CampaignStat.Bankruptcies);
    }
}
