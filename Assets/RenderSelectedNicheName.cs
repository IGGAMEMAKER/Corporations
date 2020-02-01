using Assets.Core;
using Assets.Core.Formatting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSelectedNicheName : ParameterView
{
    public override string RenderValue()
    {
        return "Market of " + EnumUtils.GetFormattedNicheName(ScreenUtils.GetSelectedNiche(Q));
    }
}
