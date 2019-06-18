using Assets.Classes;
using Assets.Utils;

public class CheckBrandingCampaignResources : ResourceChecker
{
    public override string GetBaseHint()
    {
        return "";
    }

    public override TeamResource GetRequiredResources()
    {
        return MarketingUtils.GetBrandingCost(GameContext, MyProductEntity);
    }
}
