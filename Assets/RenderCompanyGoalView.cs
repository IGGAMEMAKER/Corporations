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
            text += "\n" + g.GetFormattedName();
        }

        return text;
    }
}
