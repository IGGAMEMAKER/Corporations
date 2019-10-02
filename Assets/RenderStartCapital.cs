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

        return Format.Money(capital);
    }
}
