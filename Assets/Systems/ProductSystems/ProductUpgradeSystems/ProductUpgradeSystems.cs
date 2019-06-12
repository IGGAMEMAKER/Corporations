public class ProductUpgradeSystems : Feature
{
    public ProductUpgradeSystems(Contexts contexts) : base("Product Upgade Systems")
    {
        Add(new ProductExecuteUpgradeEvent(contexts));
    }
}