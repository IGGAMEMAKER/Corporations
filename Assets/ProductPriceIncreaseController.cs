public class ProductPriceIncreaseController : ButtonController
{
    public override void Execute()
    {
        TriggerEventIncreasePrice(ControlledProduct.Id);
    }
}
