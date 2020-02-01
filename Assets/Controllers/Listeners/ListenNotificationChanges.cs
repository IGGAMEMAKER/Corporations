using Assets.Core;
using System.Collections.Generic;

public class ListenNotificationChanges : Controller
    , IAnyNotificationsListener
{
    public override void AttachListeners()
    {
        NotificationUtils.Subscribe(Q, this);
    }

    public override void DetachListeners()
    {
        NotificationUtils.UnSubscribe(Q, this);
    }

    void IAnyNotificationsListener.OnAnyNotifications(GameEntity entity, List<NotificationMessage> notifications)
    {
        Render();
    }
}
