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
        var profit = Economy.GetProfit(gameContext, company, true);

        Debug.Log(profit.ToString()); // $"Economy: <color=green>Income</color> +{Format.MinifyMoney(income)}\n<color=red>Maintenance</color> - {maintenance.MinifyValues().ToString(true)}");

        //if (company.hasProduct)
        //{
        //    var maintenance = Economy.GetProductCompanyMaintenance(company, gameContext, true);
        //    var income = Economy.GetProductIncome(company);

        //}

        if (Companies.IsPlayerCompany(company))
        {
            Debug.Log("Player bankrupt");
            Debug.Log("My company stats");
            Debug.Log(string.Join("\n", company.companyResourceHistory.Actions.Select(r => r.Print())));

            foreach (var d in Companies.GetDaughters(gameContext, company))
            {
                Debug.Log($"Daughter {d.company.Name} stats");
                Debug.Log(string.Join("\n", d.companyResourceHistory.Actions.Select(r => r.Print())));
            }

            NotificationUtils.AddPopup(gameContext, new PopupMessageGameOver(company.company.Id));
        }

        Companies.CloseCompany(gameContext, company);
        ScheduleUtils.TweakCampaignStats(gameContext, CampaignStat.Bankruptcies);
    }
}
