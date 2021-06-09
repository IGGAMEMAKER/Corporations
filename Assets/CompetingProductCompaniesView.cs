using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompetingProductCompaniesView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var competitors = Companies.GetDirectCompetitors(Flagship, Q, true);
        var upgrades = competitors.Select(c => Visuals.Colorize(c.company.Name, c.isFlagship ? Colors.COLOR_BEST : Colors.COLOR_NEUTRAL));

        var space = "      ";

        return string.Join($"{space}|{space}", upgrades);
    }
}
