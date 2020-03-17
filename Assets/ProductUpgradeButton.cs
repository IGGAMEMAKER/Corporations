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
    public abstract long GetCost();
    public override bool IsInteractable() => true;

    public abstract string GetHint();

    bool state => Products.IsUpgradeEnabled(flagship, upgrade);
    GameEntity flagship => Companies.GetFlagship(Q, Group);

    ProductUpgrade upgrade => GetProductUpgrade();

    public override void Execute()
    {
        if (flagship != null)
        {
            Debug.Log("Toggle " + upgrade + " = " + state);

            flagship.productUpgrades.upgrades[upgrade] = !state;
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var title = GetButtonTitle();

        var cost = GetCost() * Balance.PERIOD / 30;

        if (cost != 0)
            title += " " + Format.Money(cost);

        GetComponentInChildren<TextMeshProUGUI>().text = title;
        GetComponentInChildren<Toggle>().isOn = state;

        //GetComponentInChildren<ToggleAnim>().AnimateToggle();
        Debug.Log("Upgrade : " + upgrade + " = " + state);

        var anim = GetComponentInChildren<ToggleAnim>(); //.AnimateToggle();

        anim.toggleAnimator.Play(state ? anim.toggleOn : anim.toggleOff);

        // hint
        var hint = GetComponent<Hint>();
        if (hint != null)
            hint.SetHint(GetHint());
    }
};