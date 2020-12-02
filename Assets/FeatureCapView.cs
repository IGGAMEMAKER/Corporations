using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureCapView : ParameterView
{
    public override string RenderValue()
    {
        var cap = Teams.GetMaxFeatureRatingCap(Flagship, Q).Sum();

        Colorize((int)(cap * 10), 0, 100);

        return cap.ToString("0.0") + "LV";
    }
}
