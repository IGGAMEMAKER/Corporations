using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyBalanceChange : View
{
    public ColoredValuePositiveOrNegative Change;
    public Hint Hint;

    public override void ViewRender()
    {
        base.ViewRender();

        var income = CompanyEconomyUtils.GetCompanyIncome(SelectedCompany, GameContext);
        var maintenance = CompanyEconomyUtils.GetCompanyMaintenance(SelectedCompany, GameContext);

        Change.UpdateValue(CompanyEconomyUtils.GetBalanceChange(SelectedCompany, GameContext));
        Change.Prettify = true;


        Hint.SetHint(
            "Income: " + Visuals.Positive(Format.Money(income)) + "\n" +
            "Maintenance: " + Visuals.Negative(Format.Money(maintenance))
            );
    }
}
