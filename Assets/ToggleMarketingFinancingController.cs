using Assets.Utils;

public class ToggleMarketingFinancingController : ButtonController
{
    public MarketingFinancing marketingFinancing;

    public override void Execute()
    {
        MarketingUtils.SetFinancing(GameContext, MyProductEntity.company.Id, marketingFinancing);
    }
}
