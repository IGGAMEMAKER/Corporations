using Assets.Core;

public class OpenAudienceMapButton : ButtonController
{
    public override void Execute()
    {
        OpenUrl("/Positioning");
        ScheduleUtils.PauseGame(Q);
    }
}
