using Assets.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationContainer : View
    , IAnyNotificationsListener
{
    public Text MessagesCount;
    public GameObject ClearNotificationsButton;

    void IAnyNotificationsListener.OnAnyNotifications(GameEntity entity, List<NotificationMessage> notifications)
    {
        Render();
    }

    void Start()
    {
        NotificationUtils.SubscribeToChanges(GameContext, this);
    }

    void Render()
    {
        int count = NotificationUtils.GetNotifications(GameContext).Count;

        MessagesCount.text = count.ToString();

        //ClearNotificationsButton.SetActive(count > 0);

        Animate(MessagesCount);
    }
}
