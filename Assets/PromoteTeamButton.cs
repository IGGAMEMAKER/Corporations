using Assets.Utils;

public class PromoteTeamButton : ButtonController
{
    public override void Execute()
    {
        TeamUtils.Promote(MyProductEntity);
    }
}
