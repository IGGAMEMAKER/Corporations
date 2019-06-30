using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderCompanyBalanceChange : View
{
    public ColoredValuePositiveOrNegative Change;
    public Hint Hint;

    public override void ViewRender()
    {
        base.ViewRender();

        var income = CompanyEconomyUtils.GetCompanyIncome(SelectedCompany, GameContext);
        var maintenance = CompanyEconomyUtils.GetCompanyMaintenance(SelectedCompany, GameContext);

        var change = CompanyEconomyUtils.GetBalanceChange(SelectedCompany, GameContext);

        Change.UpdateValue(change);
        Change.shorten = true;

        Change.GetComponent<Text>().text = Format.Money(change);

        Hint.SetHint(
            "Income: " + Visuals.Positive(Format.Money(income)) + "\n" +
            "Maintenance: " + Visuals.Negative(Format.Money(maintenance))
            );
    }
}
