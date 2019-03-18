public class ProductUpgradeSystems : Feature
{
    public ProductUpgradeSystems(Contexts contexts) : base("Product Upgade Systems")
    {
        // Adds task when upgrade product button is pressed
        Add(new ProductUpgradeHandlerSystem(contexts));

        Add(new ProductDevelopmentSystem(contexts));

        // updates product data
        Add(new ProductProcessUpgradeEvent(contexts));
    }
}
