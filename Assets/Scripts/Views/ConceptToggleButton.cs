using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConceptToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Upgrade concept";

    public override long GetCost()
    {
        return 0;
    }

    public override string GetHint()
    {
        return "";
    }

    public override ProductUpgrade GetProductUpgrade()
    {
        return ProductUpgrade.SimpleConcept;
    }
}
