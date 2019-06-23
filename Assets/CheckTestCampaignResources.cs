using Assets.Classes;
using Assets.Utils;

public class CheckTestCampaignResources : ResourceChecker
{
    public override string GetBaseHint()
    {
        return "Test campaign gives you a lot of feedback (ideas) and small amount of userss";
    }

    public override TeamResource GetRequiredResources()
    {
        return MarketingUtils.GetTestCampaignCost(GameContext, MyProductEntity);
    }
}
