using Assets.Core;

public class SupportToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Product support";
    public override string GetBenefits()
    {
        return Visuals.Positive($"-1% Churn");
    }

    public override ProductUpgrade upgrade => ProductUpgrade.Support;
}
