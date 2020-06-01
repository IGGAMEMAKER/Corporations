using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagshipRelayInCompanyView : View
{
    public GameObject Upgrades;
    public GameObject MarketingUpgrades;

    public void ChooseMarketingUpgrades()
    {
        Draw(MarketingUpgrades, true);
        Draw(Upgrades, false);
    }

    public void ChooseUpgrades()
    {
        Draw(MarketingUpgrades, false);
        Draw(Upgrades, true);
    }
}
