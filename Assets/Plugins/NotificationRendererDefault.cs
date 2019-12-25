using UnityEngine;

public class NotificationRendererDefault : NotificationRenderer<NotificationMessage>
{
    public override string GetDescription(NotificationMessage message)
    {
        return message.ToString();
    }

    public override string GetTitle(NotificationMessage message)
    {
        return $"Unknown Notification: {message.NotificationType}: {message.ToString()}";
    }

    public override void SetLink(NotificationMessage message, GameObject LinkToEvent)
    {
    }
}
