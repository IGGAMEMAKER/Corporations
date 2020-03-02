using Assets.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ProductUpgradeButton : UpgradedButtonController
{
    public abstract ProductUpgrade GetProductUpgrade();
    public TimedButton TimedButton;

    public abstract string GetButtonTitle();
    public override bool IsInteractable() => true;

    bool state => Products.IsUpgradeEnabled(flagship, GetProductUpgrade());
    GameEntity flagship => Companies.GetFlagship(Q, Group);

    public override void Execute()
    {
        if (flagship != null)
        {
            flagship.productUpgrades.upgrades[GetProductUpgrade()] = !state;
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        GetComponentInChildren<TextMeshProUGUI>().text = GetButtonTitle();
        GetComponentInChildren<Toggle>().isOn = state;

        Debug.Log("Upgrade : " + GetProductUpgrade() + " = " + state);

        if (state)
        {
            TimedButton.gameObject.SetActive(false);
            TimedButton.Execute();
        }
    }
};