using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderChurnRate : UpgradedParameterView
{
    public override string RenderHint()
    {
        return MarketingUtils.GetChurnBonus(GameContext, SelectedCompany.company.Id).ToString();
    }

    public override string RenderValue()
    {
        return MarketingUtils.GetChurnRate(GameContext, SelectedCompany.company.Id).ToString();
        //return MarketingUtils.GetChurnBonus(GameContext, SelectedCompany.company.Id).Sum();
    }
}
