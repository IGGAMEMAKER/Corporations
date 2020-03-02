using Assets.Core;
using TMPro;
using UnityEngine.UI;

public class ProductUpgradeButton : UpgradedButtonController
{
    public ProductUpgrade ProductUpgrade;
    public Toggle checkbox;

    public override void Execute()
    {
        if (flagship != null)
        {
            flagship.productUpgrades.upgrades[ProductUpgrade] = !checkbox.isOn;
            //flagship.productUpgrades.upgrades[ProductUpgrade] = !flagship.productUpgrades.upgrades[ProductUpgrade];
        }
    }

    GameEntity flagship => Companies.GetFlagship(Q, Group);

    public override bool IsInteractable()
    {
        return true;
    }

    public override void ViewRender()
    {
        base.ViewRender();

        checkbox = GetComponentInChildren<Toggle>();

        GetComponentInChildren<TextMeshProUGUI>().text = "Targ campaign (30K)";

        checkbox.isOn = flagship.productUpgrades.upgrades[ProductUpgrade];
    }
};