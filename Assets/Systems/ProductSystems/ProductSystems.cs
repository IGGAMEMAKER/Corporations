public class ProductSystems : Feature
{
    public ProductSystems(Contexts contexts) : base("Product Systems")
    {
        Add(new ProductInitializerSystem(contexts));

        //Add(new LogProductChangesSystem(contexts));

        Add(new ProductMarketingSystems(contexts));

        Add(new ProductResourceSystems(contexts));
    }
}
