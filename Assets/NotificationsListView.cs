using Assets.Utils;
using UnityEngine;

public class NotificationsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<NotificationView>().SetMessage(entity as NotificationMessage);
    }

    void Start()
    {
        Render();
    }

    void Render()
    {
        var notifications = NotificationUtils.GetNotifications(GameContext);

        SetItems(notifications.ToArray());
    }
}
