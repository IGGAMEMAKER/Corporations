using Assets.Core;

public class SupportToggleButton3 : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Product support (III)";
    public override string GetBenefits()
    {
        return Visuals.Positive($"-3% Churn");
    }

    public override ProductUpgrade upgrade => ProductUpgrade.Support3;
}
