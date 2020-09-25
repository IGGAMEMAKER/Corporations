using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using System.Linq;

public class FeedbackView : View
{
    public NotificationView NotificationView;

    public override void ViewRender()
    {
        base.ViewRender();

        var notifications = NotificationUtils.GetNotifications(Q);

        if (notifications.Count > 0)
        {
            notifications.Reverse();

            Show(NotificationView);
            NotificationView.SetMessage(notifications.First());
        }
        else
        {
            Hide(NotificationView);
        }
    }
}
