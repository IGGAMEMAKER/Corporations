
public class ProductMarketingSystems : Feature
{
    public ProductMarketingSystems(Contexts contexts) : base("Product Marketing Systems")
    {
        // Adds task when targeting button is pressed
        Add(new ProductToggleTargetingHandlerSystem(contexts));

        // gives you clients if you have targeting enabled
        Add(new ProductGrabClientsByTargetingSystem(contexts));

        // promote and leave clients
        Add(new ProductMoveClientsAtPeriodEnd(contexts));
    }
}
