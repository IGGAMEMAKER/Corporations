public class ProductUpgradeSystems : Feature
{
    public ProductUpgradeSystems(Contexts contexts) : base("Product Upgade Systems")
    {
        Add(new ProductDevelopmentSystem(contexts));

        // updates product data
        Add(new ProductExecuteUpgradeEvent(contexts));
    }
}