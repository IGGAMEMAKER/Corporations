public class ToggleMarketingFinancingView : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var c = GetComponent<ToggleMarketingFinancingController>();

        if (HasProductCompany)
            ToggleIsChosenComponent(MyProductEntity.finance.marketingFinancing == c.marketingFinancing);
    }
}
