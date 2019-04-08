public class UpgradeProductController : ButtonController
{
    public override void Execute()
    {
        TriggerEventUpgradeProduct(MyProduct.Id, MyProduct.ProductLevel);
    }
}