using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderGroupProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
        var income = CompanyEconomyUtils.GetCompanyIncome(SelectedCompany, GameContext);
        var maintenance = CompanyEconomyUtils.GetCompanyMaintenance(SelectedCompany, GameContext);

        //CompanyEconomyUtils.incom

        var hint = "Income: " + Visuals.Positive(Format.Money(income)) + "\n";
            //"Team Maintenance: " + Visuals.Negative(Format.Money(maintenance)) +

        return hint;
    }

    public override string RenderValue()
    {
        var change = CompanyEconomyUtils.GetBalanceChange(SelectedCompany, GameContext);

        return Visuals.PositiveOrNegativeMinified(change);
    }
}
