﻿using Assets.Core;
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
            NotificationView.SetMessage(notifications.First(), 0);
        }
        else
        {
            Hide(NotificationView);
        }
    }
}
