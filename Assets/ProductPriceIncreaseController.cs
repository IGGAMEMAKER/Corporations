public class ProductPriceIncreaseController : ButtonController
{
    public Pricing priceLevel;

    public override void Execute()
    {
        TriggerEventSetPrice(MyProduct.Id, priceLevel);

        Render();
    }

    void Render()
    {
        AddIsChosenComponent(MyProductEntity.finance.price == priceLevel);
    }
}
