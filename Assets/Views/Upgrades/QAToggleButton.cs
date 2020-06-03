using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QAToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Design Quality";
    public override string GetBenefits()
    {
        return Visuals.Positive($"-1% Churn");
    }

    public override ProductUpgrade upgrade => ProductUpgrade.QA;
}
