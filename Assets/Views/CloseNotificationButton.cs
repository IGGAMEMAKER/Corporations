using Assets.Core;

public class CloseNotificationButton : ButtonController
{
    public int NotificationId;

    public override void Execute()
    {
        NotificationUtils.ClearNotification(Q, NotificationId);
        GetComponentInParent<NotificationsListView2>().ViewRender();
        //Hide(gameObject);
    }
}
