using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTweakButtonsIfCultureCooldownIsActive : View
{
    public GameObject TweakLeft;
    public GameObject TweakRight;

    public CorporatePolicy CorporatePolicy;

    public override void ViewRender()
    {
        base.ViewRender();

        bool hasCooldown = Cooldowns.HasCorporateCultureUpgradeCooldown(Q, MyCompany);

        var culture = Companies.GetOwnCorporateCulture(MyCompany);

        var value = Companies.GetPolicyValue(MyCompany, CorporatePolicy);

        TweakLeft.GetComponent<TweakCorporatePolicy>().SetSettings(CorporatePolicy, false);
        TweakRight.GetComponent<TweakCorporatePolicy>().SetSettings(CorporatePolicy, true);

        RenderTweak(TweakLeft, hasCooldown, value, false);
        RenderTweak(TweakRight, hasCooldown, value, true);
    }

    private void RenderTweak(GameObject tweakButton, bool hasCooldown, int value, bool Increment)
    {
        bool willExceedLimits = (Increment && value == C.CORPORATE_CULTURE_LEVEL_MAX) || (!Increment && value == C.CORPORATE_CULTURE_LEVEL_MIN);

        tweakButton.SetActive(!hasCooldown && !willExceedLimits);
    }
}
