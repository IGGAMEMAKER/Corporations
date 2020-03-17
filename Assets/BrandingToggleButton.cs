using Assets.Core;

public class BrandingToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Branding campaign";

    public override long GetCost()
    {
        var flagship = Companies.GetFlagship(Q, Group);

        return Products.GetUpgradeCost(flagship, Q, GetProductUpgrade());
    }

    public override ProductUpgrade GetProductUpgrade()
    {
        return ProductUpgrade.BrandCampaign;
    }
}

