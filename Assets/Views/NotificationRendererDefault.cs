using Assets.Core;
using UnityEngine;

public class NotificationRendererDefault : NotificationRenderer<NotificationMessage>
{
    public override string GetDescription(NotificationMessage message)
    {
        return message.ToString();
    }

    public override Color GetNewsColor(NotificationMessage message)
    {
        return Visuals.GetColorFromString(Colors.COLOR_PANEL_BASE);
    }

    public override string GetTitle(NotificationMessage message)
    {
        return $"Unknown Notification: {message.NotificationType}: {message.ToString()}";
    }

    public override void SetLink(NotificationMessage message, GameObject LinkToEvent)
    {
    }
}
