using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
