using Assets.Core;
using Assets.Core;

public class CheckTestCampaignResources : ResourceChecker
{
    public override string GetBaseHint()
    {
        return "Test campaign gives you a lot of feedback (ideas) and small amount of userss";
    }

    public override TeamResource GetRequiredResources()
    {
        return new TeamResource(-1, -1, -1, -1, -1);
        //return MarketingUtils.GetTestCampaignCost(GameContext, MyProductEntity);
    }
}
