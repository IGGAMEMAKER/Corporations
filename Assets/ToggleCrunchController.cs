using Assets.Utils;

public class ToggleCrunchController : ButtonController
{
    public override void Execute()
    {
        TeamUtils.ToggleCrunching(GameContext, MyProductEntity.company.Id);

        //ReNavigate();
    }
}
