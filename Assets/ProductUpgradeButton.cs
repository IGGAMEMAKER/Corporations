using Assets.Core;
using Michsky.UI.Frost;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ProductUpgradeButton : UpgradedButtonController
{
    public abstract ProductUpgrade GetProductUpgrade();
    public TimedButton TimedButton;

    public abstract string GetButtonTitle();
    public override bool IsInteractable() => true;

    bool state => Products.IsUpgradeEnabled(flagship, upgrade);
    GameEntity flagship => Companies.GetFlagship(Q, Group);

    ProductUpgrade upgrade => GetProductUpgrade();

    public override void Execute()
    {
        if (flagship != null)
        {
            Debug.Log("Toggle " + upgrade + " = " + state);

            GetComponentInChildren<ToggleAnim>().AnimateToggle();
            flagship.productUpgrades.upgrades[upgrade] = !state;
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        GetComponentInChildren<TextMeshProUGUI>().text = GetButtonTitle();
        GetComponentInChildren<Toggle>().isOn = state;

        Debug.Log("Upgrade : " + upgrade + " = " + state);

        if (state)
        {
            TimedButton.gameObject.SetActive(false);

            var interactable = TimedButton.GetComponentInChildren<Button>().interactable;

            if (interactable)
                TimedButton.Execute();
        }
    }
};