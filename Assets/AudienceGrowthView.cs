using System.Collections;
using System.Collections.Generic;
using Assets.Core;
using UnityEngine;

public class AudienceGrowthView : UpgradedParameterView
{
    public override string RenderValue()
    {
        var growth = Marketing.GetAudienceGrowth(Flagship, Q);

        return Format.Minify(growth);
    }

    public override string RenderHint()
    {
        return "";
    }
}