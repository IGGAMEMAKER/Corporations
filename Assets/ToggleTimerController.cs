using Assets.Core;

public class ToggleTimerController : ButtonController
{
    public override void Execute()
    {
        ScheduleUtils.ToggleTimer(GameContext);
    }
}
