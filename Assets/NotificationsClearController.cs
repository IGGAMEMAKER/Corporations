using Assets.Core;

public class NotificationsClearController : ButtonController
{
    public override void Execute()
    {
        NotificationUtils.ClearNotifications(GameContext);
    }
}
