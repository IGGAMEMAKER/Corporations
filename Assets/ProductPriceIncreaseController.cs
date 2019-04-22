public class ProductPriceIncreaseController : ButtonController
{
    public int priceLevel;

    public override void Execute()
    {
        TriggerEventSetPrice(MyProduct.Id, priceLevel);
    }
}
