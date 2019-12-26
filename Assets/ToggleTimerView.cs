using Assets.Core;
using UnityEngine.UI;

public class ToggleTimerView : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var isRunning = ScheduleUtils.IsTimerRunning(GameContext);

        GetComponentInChildren<Text>().text = isRunning ? "PAUSE" : "CONTINUE";
    }
}
