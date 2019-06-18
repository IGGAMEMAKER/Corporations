using Assets.Classes;
using Assets.Utils;

public class CheckBrandingCampaignResources : CheckButton
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
