using Assets.Core;

public class TargetingToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Targeting campaign";

    public override long GetCost()
    {
        var flagship = Companies.GetFlagship(Q, Group);

        return Products.GetUpgradeCost(flagship, Q, GetProductUpgrade());
    }

    public override string GetBenefits()
    {
        var flagship = Companies.GetFlagship(Q, Group);
        var clients = Marketing.GetTargetingCampaignGrowth(flagship, Q);

        return Visuals.Positive($"+{clients}") + " users weekly";
    }

    public override ProductUpgrade GetProductUpgrade()
    {
        return ProductUpgrade.TargetingInSocialNetworks;
    }
}
