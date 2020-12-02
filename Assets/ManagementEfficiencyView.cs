using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagementEfficiencyView : ParameterView
{
    public override string RenderValue()
    {
        var efficiency = Companies.GetManagementEfficiency(MyCompany, Q);

        Colorize(efficiency, 0, 100);

        return efficiency + "%";
    }
}
