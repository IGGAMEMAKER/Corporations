using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderNecessaryAmountOfWorkers : ParameterView
{
    public override string RenderValue()
    {
        var required = Economy.GetNecessaryAmountOfWorkers(SelectedCompany, GameContext);

        var have = TeamUtils.GetAmountOfWorkers(SelectedCompany, GameContext);

        bool isEnough = have >= required;

        Colorize(isEnough ? VisualConstants.COLOR_NEUTRAL : VisualConstants.COLOR_NEGATIVE);

        return $"{have} / {required}";
    }
}
