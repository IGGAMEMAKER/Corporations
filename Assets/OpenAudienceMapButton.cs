using Assets.Core;

public class OpenAudienceMapButton : ButtonController
{
    public override void Execute()
    {
        OpenUrl("/Holding/Positioning");
        ScheduleUtils.PauseGame(Q);
    }
}
