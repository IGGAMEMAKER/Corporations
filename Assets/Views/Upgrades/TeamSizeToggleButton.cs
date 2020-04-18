using Assets.Core;

public class TeamSizeToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Reorganize team";

    public override long GetCost()
    {
        return Products.GetUpgradeCost(Flagship, Q, upgrade);
    }

    public override string GetBenefits()
    {
        return "Increases worker limit";
    }

    public override ProductUpgrade upgrade => ProductUpgrade.Team3;
}
