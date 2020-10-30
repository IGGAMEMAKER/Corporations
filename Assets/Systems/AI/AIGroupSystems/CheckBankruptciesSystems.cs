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
        var profit = Economy.GetProfit(gameContext, company, true);

        Companies.Log(company, $"Company {company.company.Name} BANKRUPT");
        Companies.Log(company, "Profit: " + profit.ToString()); // $"Economy: <color=green>Income</color> +{Format.MinifyMoney(income)}\n<color=red>Maintenance</color> - {maintenance.MinifyValues().ToString(true)}");

        Companies.LogFinancialTransactions(company);

        foreach (var d in Companies.GetDaughters(gameContext, company))
        {
            Companies.Log(company, $"* Daughter {d.company.Name} stats");
            Companies.LogFinancialTransactions(d);
        }

        if (Companies.IsPlayerCompany(company))
        {
            NotificationUtils.AddPopup(gameContext, new PopupMessageGameOver(company.company.Id));
        }

        Companies.CloseCompany(gameContext, company, true);
        Companies.Log(company, $"Company {company.company.Name} CLOSED");
        ScheduleUtils.TweakCampaignStats(gameContext, CampaignStat.Bankruptcies);
    }
}
