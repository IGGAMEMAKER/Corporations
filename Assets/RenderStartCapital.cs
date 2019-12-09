using Assets.Utils;
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
        var capital = NicheUtils.GetStartCapital(SelectedNiche, GameContext);

        return Visuals.Colorize(Format.Money(capital), Companies.IsEnoughResources(MyCompany, capital));
    }
}
