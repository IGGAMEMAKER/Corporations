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
        var marketing = CompanyEconomyUtils.GetMarketingMaintenance(SelectedCompany, GameContext);

        var change = CompanyEconomyUtils.GetBalanceChange(SelectedCompany, GameContext) - marketing;

        Change.UpdateValue(change);
        Change.shorten = true;

        //Change.GetComponent<Text>().text = Format.Money(change);

        var marketingMaintenance = "";
        if (SelectedCompany.hasProduct)
            marketingMaintenance = "\nMarketing Expenses: " + Visuals.Negative(Format.Money(marketing));

        Hint.SetHint(
            "Income: " + Visuals.Positive(Format.Money(income)) + "\n" +
            "Team Maintenance: " + Visuals.Negative(Format.Money(maintenance)) + 
            marketingMaintenance
            );
    }
}
