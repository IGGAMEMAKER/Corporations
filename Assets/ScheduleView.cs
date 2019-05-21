using Assets.Utils;
using UnityEngine.UI;

public class ScheduleView : View
{
    public ResourceView ScheduleResourceView;
    public Text PauseStatus;

    public override void ViewRender()
    {
        base.ViewRender();

        ScheduleResourceView.UpdateResourceValue("Date", CurrentIntDate);

        PauseStatus.gameObject.SetActive(!ScheduleUtils.IsTimerRunning(GameContext));
    }
}
