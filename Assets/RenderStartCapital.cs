using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderStartCapital : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var capital = Markets.GetStartCapital(SelectedNiche, Q);

        return Visuals.Colorize(Format.Money(capital), Companies.IsEnoughResources(MyCompany, capital));
    }
}
