using Assets.Core;

public class CloseNotificationButton : ButtonController
{
    public int NotificationId;

    public override void Execute()
    {
        NotificationUtils.ClearNotification(Q, NotificationId);

        if (NotificationUtils.GetNotifications(Q).Count > 0)
        {
            GetComponentInParent<NotificationsListView2>().ViewRender();
        }
        else
        {
            HideNotificationMenu();
        }
    }
}
