using Assets.Core;

public class TargetingToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle()
    {
        var flagship = Companies.GetFlagship(Q, Group);

        var cost = Marketing.GetTargetingCost(flagship, Q);

        return $"Targeting campaign ({Format.Money(cost)})";
    }

    public override ProductUpgrade GetProductUpgrade()
    {
        return ProductUpgrade.TargetingInSocialNetworks;
    }
}
