using Assets.Core;

public class TargetingToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Targeting campaign";

    public override long GetCost()
    {
        var flagship = Companies.GetFlagship(Q, Group);

        return Products.GetUpgradeCost(flagship, Q, GetProductUpgrade());
    }

    public override ProductUpgrade GetProductUpgrade()
    {
        return ProductUpgrade.TargetingInSocialNetworks;
    }
}
