using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrandChangeDescription : UpgradedParameterView
{
    public override string RenderHint()
    {
        return BrandPower.ToString();
    }

    public override string RenderValue()
    {
        return "Brand power: " + (int)SelectedCompany.branding.BrandPower;
    }

    Bonus BrandPower => MarketingUtils.GetMonthlyBrandPowerChange(SelectedCompany, GameContext);
}
