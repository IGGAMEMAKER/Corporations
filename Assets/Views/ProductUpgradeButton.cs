using Assets.Core;
using Michsky.UI.Frost;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ProductUpgradeButton : UpgradedButtonController
{
    public abstract ProductUpgrade upgrade { get; }

    public abstract string GetButtonTitle();
    public abstract long GetCost();
    public override bool IsInteractable() => true;

    public abstract string GetBenefits();

    bool state => Products.IsUpgradeEnabled(Flagship, upgrade);

    public override void Execute()
    {
        if (Flagship != null)
        {
            Debug.Log("Toggle " + upgrade + " = " + state);

            Products.SetUpgrade(Flagship, upgrade, !state);
            //flagship.productUpgrades.upgrades[upgrade] = !state;
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        // checkbox text
        var title = GetButtonTitle();

        GetComponentInChildren<TextMeshProUGUI>().text = title + "\n" + GetBenefits();

        
        // proper animation
        GetComponentInChildren<Toggle>().isOn = state;
        var anim = GetComponentInChildren<ToggleAnim>();

        anim.toggleAnimator.Play(state ? anim.toggleOn : anim.toggleOff);

        
        
        // hint
        var hint = GetComponent<Hint>();
        if (hint != null)
        {
            var cost = GetCost() * Balance.PERIOD / 30;
            var text = "";

            if (cost != 0)
                text += "This will cost you " + Visuals.Colorize(Format.Money(cost), Economy.IsCanMaintain(MyCompany, Q, cost));
            else
                text += "This action is " + Visuals.Positive("FREE");

            hint.SetHint(text);
        }
    }
};