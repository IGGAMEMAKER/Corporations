using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QAToggleButton2 : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Design Quality (II)";
    public override string GetBenefits()
    {
        return Visuals.Positive($"-1% Brand Decay");
    }

    public override ProductUpgrade upgrade => ProductUpgrade.QA2;
}
