using Assets.Utils;

public class NotificationsClearController : ButtonController
{
    public override void Execute()
    {
        NotificationUtils.ClearNotifications(GameContext);
    }
}
