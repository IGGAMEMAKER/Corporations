public class ProductPriceChangeView : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var c = GetComponent<ProductPriceIncreaseController>();

        ToggleIsChosenComponent(MyProductEntity.finance.price == c.priceLevel);
    }
}
