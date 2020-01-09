using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LazyUpdate))]
public class TweakCorporatePolicy : UpgradedButtonController
{
    public CorporatePolicy CorporatePolicy;
    public bool Increment = true;

    public override void Execute()
    {
        if (Increment)
            Companies.IncrementCorporatePolicy(GameContext, MyCompany, CorporatePolicy);
        else
            Companies.DecrementCorporatePolicy(GameContext, MyCompany, CorporatePolicy);
    }

    public override bool IsInteractable()
    {
        bool hasCooldown = Cooldowns.HasCorporateCultureUpgradeCooldown(GameContext, MyCompany);

        var culture = Companies.GetOwnCorporateCulture(MyCompany);

        var value = Companies.GetPolicyValue(MyCompany, CorporatePolicy);
        bool willExceedLimits = (Increment && value == Constants.CORPORATE_CULTURE_LEVEL_MAX) || (!Increment && value == Constants.CORPORATE_CULTURE_LEVEL_MIN);

        return !hasCooldown && !willExceedLimits;
    }
}
