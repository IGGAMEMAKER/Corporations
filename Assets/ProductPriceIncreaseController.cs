public class ProductPriceIncreaseController : ButtonController
{
    public Pricing priceLevel;

    public override void Execute()
    {
        TriggerEventSetPrice(MyProduct.Id, priceLevel);
    }
}
