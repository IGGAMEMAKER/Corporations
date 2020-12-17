using Assets.Core;

public class QAToggleButton2 : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Design Quality (II)";
    public override string GetBenefits()
    {
        return Visuals.Positive($"-1% Churn");
    }

    public override ProductUpgrade upgrade => ProductUpgrade.QA2;
}
