using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManagersTabName : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var strength = Teams.GetTeamAverageStrength(SelectedCompany, Q);

        return $"TEAMS ({strength}LVL)";
    }
}
