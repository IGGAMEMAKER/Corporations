using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMarketDemand : ParameterView
{
    public override string RenderValue()
    {
        var demand = MarketingUtils.GetClientFlow(GameContext, SelectedNiche);

        return Format.Minify(demand) + " users";
    }
}
