using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMonthlyGrowth : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var growth = MarketingUtils.GetAudienceGrowth(SelectedCompany, GameContext);

        return Format.Minify(growth) + " users";
    }
}
