using Assets.Classes;
using Assets.Utils;

public class CheckSegmentImprovementResources : ResourceChecker
{
    public UserType UserType;

    public override string GetBaseHint()
    {
        return "Increases client loyalty";
    }

    public override TeamResource GetRequiredResources()
    {
        return ProductUtils.GetProductUpgradeCost(MyProductEntity, GameContext);
    }

    public void SetSegment(UserType userType)
    {
        UserType = userType;
    }
}
