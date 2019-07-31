using Assets.Utils;

public class UpdateSegmentController : ButtonController
{
    UserType UserType;

    public void SetSegment(UserType userType)
    {
        UserType = userType;
    }

    public override void Execute()
    {
        ProductUtils.UpdateSegment(MyProductEntity, GameContext);
    }
}
