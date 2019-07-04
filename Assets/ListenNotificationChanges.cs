using Assets.Utils;
using System.Collections.Generic;

public class ListenNotificationChanges : Controller
    , IAnyNotificationsListener
{
    public override void AttachListeners()
    {
        NotificationUtils.Subscribe(GameContext, this);
    }

    public override void DetachListeners()
    {
        NotificationUtils.UnSubscribe(GameContext, this);
    }

    void IAnyNotificationsListener.OnAnyNotifications(GameEntity entity, List<NotificationMessage> notifications)
    {
        Render();
    }
}
