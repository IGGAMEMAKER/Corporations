using Assets.Utils;
using Assets.Utils.Formatting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSelectedNicheName : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return EnumUtils.GetFormattedNicheName(ScreenUtils.GetSelectedNiche(GameContext));
    }
}
