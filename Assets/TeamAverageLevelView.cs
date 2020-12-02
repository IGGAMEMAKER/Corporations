using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAverageLevelView : ParameterView
{
    public override string RenderValue()
    {
        var efficiency = Teams.GetTeamAverageStrength(Flagship, Q);

        Colorize(efficiency, 0, 100);

        return efficiency + "LV";
    }
}
