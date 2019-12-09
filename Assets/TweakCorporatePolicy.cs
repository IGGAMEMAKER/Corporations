using Assets.Utils;
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
        bool hasCooldown = CooldownUtils.HasCorporateCultureUpgradeCooldown(GameContext, MyCompany);

        var culture = MyCompany.corporateCulture.Culture;

        var value = culture[CorporatePolicy];
        bool willExceedLimits = (Increment && value == 5) || (!Increment && value == 1);

        return !hasCooldown && !willExceedLimits;
    }
}
