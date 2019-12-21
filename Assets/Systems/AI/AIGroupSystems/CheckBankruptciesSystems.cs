using System.Collections.Generic;
using System.Linq;
using Assets.Utils;
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

    bool IsBankrupt(GameEntity company) => Companies.BalanceOf(company) < 0;

    void CloseBankruptCompanies(GameEntity company)
    {
        Debug.Log("Bankrupt: " + company.company.Name);

        if (Companies.IsPlayerCompany(gameContext, company))
            NotificationUtils.AddPopup(gameContext, new PopupMessageGameOver(company.company.Id));

        Companies.CloseCompany(gameContext, company);
    }
}
