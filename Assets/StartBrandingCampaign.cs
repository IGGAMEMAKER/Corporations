using Assets.Utils;

public class StartBrandingCampaign : ButtonController
{
    public override void Execute()
    {
        MarketingUtils.StartBrandingCampaign(GameContext, MyProductEntity);
    }
}
