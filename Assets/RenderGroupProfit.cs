using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderGroupProfit : UpgradedParameterView
{
    public override string RenderHint()
    {
        var daughters = CompanyUtils.GetDaughterCompanies(GameContext, MyCompany.company.Id);

        return string.Join("\n", daughters.Select(GetIncomeInfo));

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

    string GetIncomeInfo(GameEntity c) {

        return $"{c.company.Name}: {Format.Money(CompanyEconomyUtils.GetBalanceChange(c, GameContext))}";
    }
}
