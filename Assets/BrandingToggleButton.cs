using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrandingToggleButton : ProductUpgradeButton
{
    public override void ViewRender()
    {
        base.ViewRender();

        ProductUpgrade = ProductUpgrade.BrandCampaign;
    }
}

