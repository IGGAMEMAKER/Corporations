using Assets.Core;

public class CloseNotificationButton : ButtonController
{
    public int NotificationId;

    public override void Execute()
    {
        NotificationUtils.ClearNotification(GameContext, NotificationId);
    }
}
