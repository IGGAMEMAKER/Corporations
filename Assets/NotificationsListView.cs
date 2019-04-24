using System.Collections.Generic;
using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsListView : ListView
    , IAnyNotificationsListener
{
    public ScrollRect _sRect;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<NotificationView>().SetMessage(entity as NotificationMessage);
    }

    void Start()
    {
        NotificationUtils.SubscribeToChanges(GameContext, this);
    }

    void Render()
    {
        var notifications = NotificationUtils.GetNotifications(GameContext);

        SetItems(notifications.ToArray());
    }

    void IAnyNotificationsListener.OnAnyNotifications(GameEntity entity, List<NotificationMessage> notifications)
    {
        Render();

        _sRect.verticalNormalizedPosition = 0f;
    }
}
