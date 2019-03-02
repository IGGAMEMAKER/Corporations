public class ProductSystems : Feature
{
    public ProductSystems(Contexts contexts) : base("Product Systems")
    {
        Add(new ProductInitializerSystem(contexts));
        Add(new LogProductChangesSystem(contexts));
        
        // Adds task when upgrade product button is pressed
        Add(new ProductUpgradeEventHandler(contexts));

        // updates product data
        Add(new ProductProcessUpgradeEvent(contexts));
    }
}
