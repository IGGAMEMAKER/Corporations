public class ProductPriceIncreaseController : ButtonController
{
    public override void Execute()
    {
        TriggerEventIncreasePrice(MyProduct.Id);
    }
}
