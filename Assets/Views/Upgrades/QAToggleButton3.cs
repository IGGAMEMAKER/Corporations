﻿using Assets.Core;

public class QAToggleButton3 : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Design Quality (II)";
    public override string GetBenefits()
    {
        return Visuals.Positive($"-1% Brand Decay");
    }

    public override ProductUpgrade upgrade => ProductUpgrade.QA3;
}
