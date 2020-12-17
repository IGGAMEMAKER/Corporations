public class TeamSizeToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Reorganize team";
    public override string GetBenefits()
    {
        return "Increases max workers";
    }

    public override ProductUpgrade upgrade => ProductUpgrade.Team3;
}
