using Assets.Core;

public class OneTimeToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() {
        switch (upgrade)
        {
            case ProductUpgrade.CreateManagementTeam:
                return "Create Management Team";

            case ProductUpgrade.CreateSupportTeam:
                return "Create Support Team";

            case ProductUpgrade.CreateQATeam:
                return "Create QA Team";

            default:
                return "One Time Button? " + upgrade;
        }
    }

    public override string GetBenefits()
    {
        return Visuals.Positive($"New opportunities");
    }

    public override ProductUpgrade upgrade => OneTimeUpgrade;
    public ProductUpgrade OneTimeUpgrade;
}
