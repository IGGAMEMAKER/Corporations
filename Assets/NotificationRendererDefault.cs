using UnityEngine;
using UnityEngine.UI;

public class NotificationRendererDefault : NotificationRenderer<NotificationMessage>
{
    public static string GetTitle(NotificationMessage message, GameContext gameContext)
    {
        return $"Unknown Notification: {message.NotificationType}: {message.ToString()}";
    }

    public override void Render(NotificationMessage message, Text Title, Text Description, GameObject LinkToEvent)
    {
        Description.text = message.ToString();

        Title.text = GetTitle(message, GameContext);

        RemoveLinks();
    }
}
