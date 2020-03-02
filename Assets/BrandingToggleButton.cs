using Assets.Core;

public class BrandingToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle()
    {
        var flagship = Companies.GetFlagship(Q, Group);

        var cost = Marketing.GetBrandingCost(flagship, Q);

        return $"Branding campaign ({Format.Money(cost)})";
    }

    public override ProductUpgrade GetProductUpgrade()
    {
        return ProductUpgrade.BrandCampaign;
    }
}

