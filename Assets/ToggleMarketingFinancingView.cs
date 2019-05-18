public class ToggleMarketingFinancingView : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var c = GetComponent<ToggleMarketingFinancingController>();

        ToggleIsChosenComponent(MyProductEntity.finance.marketingFinancing == c.marketingFinancing);
    }
}
