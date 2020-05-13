using Assets.Core;
using System.Collections;
using System.Collections.Generic;

public class CEOProductUpgrades : CompanyUpgradeList
{
    public override ProductUpgrade[] GetUpgrades()
    {
        return new ProductUpgrade[]
        {
            ProductUpgrade.TestCampaign
        };
    }
}
