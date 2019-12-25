using Assets.Utils;

public class ToggleTimerController : ButtonController
{
    public override void Execute()
    {
        ScheduleUtils.ToggleTimer(GameContext);
    }
}
