using Assets.Core;

public class TargetingToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Targeting campaign";

    public override long GetCost()
    {
        var flagship = Companies.GetFlagship(Q, Group);

        return Products.GetUpgradeCost(flagship, Q, GetProductUpgrade());
    }

    public override string GetHint()
    {
        var flagship = Companies.GetFlagship(Q, Group);
        var clients = Marketing.GetAudienceGrowth(flagship, Q);

        return Visuals.Positive($"Gives you {clients} clients") + " each week";
    }

    public override ProductUpgrade GetProductUpgrade()
    {
        return ProductUpgrade.TargetingInSocialNetworks;
    }
}
