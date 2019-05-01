public class ProductUpgradeSystems : Feature
{
    public ProductUpgradeSystems(Contexts contexts) : base("Product Upgade Systems")
    {
        Add(new ProductDevelopmentSystem(contexts));

        Add(new ProductDevelopmentSegmentSystem(contexts));

        // updates product data
        Add(new ProductExecuteUpgradeEvent(contexts));
    }
}