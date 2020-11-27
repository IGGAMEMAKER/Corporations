using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyGoalView : ParameterView
{
    public override string RenderValue()
    {
        var goals = SelectedCompany.companyGoal.Goals;

        var text = "<size=40><b>Company goals</b></size>\n";

        foreach (var g in goals)
        {
            text += "\n* " + g.GetFormattedName();
        }

        var strongerCompanies = Companies.GetCompanyNames(Companies.GetStrongerCompetitors(SelectedCompany, Q, false));
        var weakerCompanies = Companies.GetCompanyNames(Companies.GetWeakerCompetitors(SelectedCompany, Q, false));

        text += $"\n\n<b>Stronger</b> than {weakerCompanies}\n\n<b>Weaker</b> than {strongerCompanies}";

        return text;
    }
}
