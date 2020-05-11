using Assets.Core;
using System.Collections;
using System.Collections.Generic;

public class MarketingProductUpgrades : CompanyUpgradeList
{
    public override ProductUpgrade[] GetUpgrades()
    {
        return new ProductUpgrade[]
        {
            ProductUpgrade.TargetingCampaign, ProductUpgrade.TargetingCampaign2, ProductUpgrade.TargetingCampaign3,
            ProductUpgrade.BrandCampaign, ProductUpgrade.BrandCampaign2, ProductUpgrade.BrandCampaign3
        };
    }
}
