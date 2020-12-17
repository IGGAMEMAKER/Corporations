using Assets.Core;
using UnityEngine;

public class ShowNotificationsController : ButtonController
{
    public GameObject Notifications;

    public override void Execute()
    {
        Show(Notifications);
        Notifications.GetComponentInChildren<NotificationsListView2>().ViewRender();
        ScheduleUtils.PauseGame(Q);
    }
}
