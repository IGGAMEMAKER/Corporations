using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMonthlyGrowth : UpgradedParameterView
{
    public override string RenderHint()
    {
        return MarketingUtils.GetAudienceGrowth(SelectedCompany, GameContext).ToString();
    }

    public override string RenderValue()
    {
        var multiplier = MarketingUtils.GetAudienceGrowthMultiplier(SelectedCompany, GameContext);
        var growth = MarketingUtils.GetAudienceGrowth(SelectedCompany, GameContext);

        return $"{Format.Minify(growth)} users (+{multiplier}%)";
    }
}
