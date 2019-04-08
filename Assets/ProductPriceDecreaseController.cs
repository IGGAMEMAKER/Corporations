public class ProductPriceDecreaseController : ButtonController
{
    public override void Execute()
    {
        TriggerEventDecreasePrice(MyProduct.Id);
    }
}