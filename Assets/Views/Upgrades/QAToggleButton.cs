using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QAToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Design Quality";

    public override long GetCost()
    {
        return Products.GetUpgradeCost(Flagship, Q, upgrade);
    }

    public override string GetBenefits()
    {
        return Visuals.Positive($"-1% Brand Decay");
    }

    public override ProductUpgrade upgrade => ProductUpgrade.QA;
}
