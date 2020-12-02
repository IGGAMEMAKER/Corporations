using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketingEfficiencyView : ParameterView
{
    public override string RenderValue()
    {
        var eff = Teams.GetMarketingEfficiency(Flagship);

        Colorize(eff, 0, 100);

        return eff + "%";
    }
}
