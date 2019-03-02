public class UpgradeProductController : ButtonController
{
    void OnUpgradeProduct()
    {
        ProductComponent product = ControlledProduct;

        StartTask().AddEventUpgradeProduct(product.Id, product.ProductLevel);
    }

    public override void Execute() => OnUpgradeProduct();
}