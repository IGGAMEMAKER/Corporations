using Assets.Core;

public class MonetisationToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Enable Monetisation";

    public override long GetCost()
    {
        return Products.GetUpgradeCost(Flagship, Q, upgrade);
    }

    public override string GetBenefits()
    {
        return Visuals.Positive($"You will start making money!");
    }

    public override ProductUpgrade upgrade => ProductUpgrade.Monetisation;
}
