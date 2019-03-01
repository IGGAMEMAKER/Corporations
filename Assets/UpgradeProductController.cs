public class UpgradeProductController : ButtonController
{
    void OnUpgradeProduct()
    {
        ProductComponent product = ControlledProduct;

        SendEvent().AddEventUpgradeProduct(product.Id, product.ProductLevel);
    }

    public override void Execute() => OnUpgradeProduct();
}