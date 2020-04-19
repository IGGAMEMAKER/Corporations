using Assets.Core;

public class SupportToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Product support";
    public override string GetBenefits()
    {
        return Visuals.Positive($"-1% Brand Decay");
    }

    public override ProductUpgrade upgrade => ProductUpgrade.Support;
}
