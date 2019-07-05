using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickMostImportantMessage : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var notifications = NotificationUtils.GetNotifications(GameContext);

        var c = GetComponent<NotificationView>();
        c.SetMessage(notifications[notifications.Count - 1]);
    }
}
