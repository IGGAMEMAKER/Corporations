public class CEOProductUpgrades : CompanyUpgradeList
{
    public override ProductUpgrade[] GetUpgrades()
    {
        return new ProductUpgrade[]
        {
            ProductUpgrade.TestCampaign
        };
    }
}
