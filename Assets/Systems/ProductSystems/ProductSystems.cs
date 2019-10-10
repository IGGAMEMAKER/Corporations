public class ProductSystems : Feature
{
    public ProductSystems(Contexts contexts) : base("Product Systems")
    {
        //Add(new LogProductChangesSystem(contexts));

        Add(new ProductResourceSystems(contexts));
    }
}
