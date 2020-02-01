using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderNecessaryAmountOfWorkers : ParameterView
{
    public override string RenderValue()
    {
        var required = Economy.GetNecessaryAmountOfWorkers(SelectedCompany, Q);

        var have = Teams.GetAmountOfWorkers(SelectedCompany, Q);

        bool isEnough = have >= required;

        Colorize(isEnough ? Colors.COLOR_NEUTRAL : Colors.COLOR_NEGATIVE);

        return $"{have} / {required}";
    }
}
