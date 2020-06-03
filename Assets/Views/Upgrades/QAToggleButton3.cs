using Assets.Core;

public class QAToggleButton3 : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Design Quality (III)";
    public override string GetBenefits()
    {
        return Visuals.Positive($"-1% Churn");
    }

    public override ProductUpgrade upgrade => ProductUpgrade.QA3;
}
