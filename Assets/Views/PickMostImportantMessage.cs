using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickMostImportantMessage : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var notification = entity as NotificationMessage;

        t.GetComponent<NotificationView>()
            .SetMessage(notification, index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var message = GetMostImportant();

        SetItems(message);
    }

    NotificationMessage[] GetMostImportant()
    {
        var notifications = NotificationUtils.GetNotifications(Q);

        if (notifications.Count > 0)
        {
            var notification = notifications[notifications.Count - 1];

            return new NotificationMessage[] { notification };
        }

        return new NotificationMessage[0];
    }
}
