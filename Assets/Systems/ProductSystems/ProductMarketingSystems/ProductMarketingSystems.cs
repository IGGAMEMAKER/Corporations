
public class ProductMarketingSystems : Feature
{
    public ProductMarketingSystems(Contexts contexts) : base("Product Marketing Systems")
    {
        // promote and leave clients
        Add(new ProductMoveClientsAtPeriodEnd(contexts));
    }
}
