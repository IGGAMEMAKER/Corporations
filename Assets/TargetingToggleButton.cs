using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingToggleButton : ProductUpgradeButton
{
    public override void ViewRender()
    {
        base.ViewRender();

        ProductUpgrade = ProductUpgrade.TargetingInSocialNetworks;
    }
}
