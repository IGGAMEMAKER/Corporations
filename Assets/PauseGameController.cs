using Assets.Core;

public class PauseGameController : ButtonController
{
    public override void Execute()
    {
        ScheduleUtils.PauseGame(Q);
    }
}
