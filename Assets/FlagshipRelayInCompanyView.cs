using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagshipRelayInCompanyView : View
{
    public GameObject Upgrades;
    public GameObject MarketingUpgrades;

    // buttons
    public GameObject UpgradeButton;
    public GameObject MarketingButton;

    private void OnEnable()
    {
        ChooseMarketingUpgrades();
    }

    public void ChooseMarketingUpgrades()
    {
        Draw(MarketingUpgrades, true);
        Draw(Upgrades, false);

        Draw(UpgradeButton, true);
        Draw(MarketingButton, false);
    }

    public void ChooseUpgrades()
    {
        Draw(MarketingUpgrades, false);
        Draw(Upgrades, true);

        Draw(UpgradeButton, false);
        Draw(MarketingButton, true);
    }
}
