public class UpgradeProductController : ButtonController
{
    public override void Execute()
    {
        TriggerEventUpgradeProduct(ControlledProduct.Id, ControlledProduct.ProductLevel);
    }
}