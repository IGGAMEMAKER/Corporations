public class UpgradeProductController : ButtonController
{
    public override void Execute()
    {
        ProductComponent product = ControlledProduct;

        TriggerEventUpgradeProduct(product.Id, product.ProductLevel);
    }
}