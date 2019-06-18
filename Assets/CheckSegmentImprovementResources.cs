using Assets.Classes;
using Assets.Utils;

public class CheckSegmentImprovementResources : ResourceChecker
{
    public UserType UserType;

    public override TeamResource GetRequiredResources()
    {
        return ProductUtils.GetSegmentUpgradeCost(MyProductEntity, GameContext, UserType);
    }

    public void SetSegment(UserType userType)
    {
        UserType = userType;
    }
}
