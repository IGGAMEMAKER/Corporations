using Assets.Core;
using Michsky.UI.Frost;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ProductUpgradeButton : UpgradedButtonController
{
    public abstract ProductUpgrade upgrade { get; }

    public abstract string GetButtonTitle();
    public abstract string GetBenefits();
    public override bool IsInteractable() => true;

    public long GetCost() => Products.GetUpgradeCost(Company, Q, upgrade);
    public long GetAmountOfWorkers() => Products.GetUpgradeWorkerCost(Company, Q, upgrade);

    bool state => Products.IsUpgradeEnabled(Company, upgrade);
    GameEntity Company => Flagship;

    //TextMeshProUGUI Title;
    //Toggle Toggle;
    //ToggleAnim ToggleAnim;
    //Hint Hint;

    //void Start()
    //{
    //    Title = GetComponentInChildren<TextMeshProUGUI>();
    //    Toggle = GetComponentInChildren<Toggle>();
    //    ToggleAnim = GetComponentInChildren<ToggleAnim>();
    //    Hint = GetComponent<Hint>();
    //}

    public override void Execute()
    {
        if (Company != null)
        {
            Debug.Log("Toggle " + upgrade + " = " + state);

            Products.SetUpgrade(Flagship, upgrade, !state);
            //flagship.productUpgrades.upgrades[upgrade] = !state;
        }
    }

    void RenderToggleState(bool state, ToggleAnim anim)
    {
        anim.toggleAnimator.Play(state ? anim.toggleOn : anim.toggleOff);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var links = GetComponent<ProductUpgradeLinks>();

        if (links == null)
            return;

        //Title = links.Title;
        //Toggle = links.Toggle;
        //ToggleAnim = links.ToggleAnim;
        //Hint = links.Hint;

        // checkbox text
        links.Title.text = GetButtonTitle() + "\n" + GetBenefits();

        
        // proper animation
        links.Toggle.isOn = state;
        var anim = links.ToggleAnim;

        if (!TutorialUtils.IsDebugMode())
            RenderToggleState(state, anim);

        var cost = GetCost() * C.PERIOD / 30;
        var text = "";

        if (cost != 0)
        {
            text += "This will cost you " + Visuals.Colorize(Format.Money(cost), Economy.IsCanMaintain(MyCompany, Q, cost));
        }

        var workers = GetAmountOfWorkers();
        if (workers > 0)
        {
            text += $"\nWill need {workers} additional workers";
        }

        links.Hint.SetHint(text);
    }
};