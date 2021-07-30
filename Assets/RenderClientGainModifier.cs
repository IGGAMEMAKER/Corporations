using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderClientGainModifier : UpgradedParameterView2<Bonus<long>>
{
    public override Bonus<long> GetValue()
    {
        return Marketing.GetMarketingEfficiency(Flagship);
    }

    public override string RenderHint()
    {
        return value.SortByModule().ToString();
    }

    public override string RenderValue()
    {
        var sum = value.Sum();

        Colorize((int)sum, 0, 250);

        return "+" + value.Sum() + "%";
    }
}
