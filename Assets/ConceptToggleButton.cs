using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConceptToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle()
    {
        var flagship = Companies.GetFlagship(Q, Group);

        return $"Upgrade concept";
    }

    public override ProductUpgrade GetProductUpgrade()
    {
        return ProductUpgrade.SimpleConcept;
    }
}
