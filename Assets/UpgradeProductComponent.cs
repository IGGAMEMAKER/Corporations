using Entitas;

public class UpgradeProductComponent : ButtonController
{
    void OnUpgradeProduct()
    {
        ProductComponent product = ControlledProduct;

        SendEvent().AddEventUpgradeProduct(ControlledProduct.Id, ControlledProduct.ProductLevel);
    }

    public override void Execute() => OnUpgradeProduct();
}
