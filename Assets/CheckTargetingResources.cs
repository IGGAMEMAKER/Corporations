using Assets.Classes;
using Assets.Utils;

public class CheckTargetingResources : ResourceChecker
{
    public override string GetBaseHint()
    {
        return "Will give you clients everyday";
    }

    public override TeamResource GetRequiredResources()
    {
        return MarketingUtils.GetTargetingCost(GameContext, MyProductEntity.company.Id);
    }
}
