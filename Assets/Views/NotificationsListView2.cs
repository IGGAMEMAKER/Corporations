using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsListView2 : ListView
{
    public Text MessagesCount;

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<NotificationView>().SetMessage(entity as NotificationMessage, index);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var notifications = NotificationUtils.GetNotifications(Q);

        MessagesCount.text = $"Messages: {notifications.Count}";

        SetItems(notifications);
    }

    private void OnEnable()
    {
        ViewRender();
    }

    public void CloseNotification(int notificationId)
    {
        ViewRender();
    }
}
