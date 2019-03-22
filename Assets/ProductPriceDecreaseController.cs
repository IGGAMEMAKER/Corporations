public class ProductPriceDecreaseController : ButtonController
{
    public override void Execute()
    {
        TriggerEventDecreasePrice(ControlledProduct.Id);
    }
}