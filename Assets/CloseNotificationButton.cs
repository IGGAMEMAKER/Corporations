using Assets.Utils;

public class CloseNotificationButton : ButtonController
{
    public int NotificationId;

    public override void Execute()
    {
        NotificationUtils.ClearNotification(GameContext, NotificationId);
    }
}
