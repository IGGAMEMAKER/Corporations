using Assets.Core;

public class QAToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Design Quality";
    public override string GetBenefits()
    {
        return Visuals.Positive($"-1% Churn");
    }

    public override ProductUpgrade upgrade => ProductUpgrade.QA;
}
