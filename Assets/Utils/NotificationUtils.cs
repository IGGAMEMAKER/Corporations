using Entitas;
using System.Collections.Generic;

namespace Assets.Utils
{
    public static class NotificationUtils
    {
        private static GameEntity GetNotificationsComponent(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Notifications)[0];
        }

        public static List<NotificationMessage> GetNotifications(GameContext gameContext)
        {
            return GetNotificationsComponent(gameContext).notifications.Notifications;
        }

        public static void ClearMessages(GameContext gameContext)
        {
            var n = GetNotificationsComponent(gameContext);

            var l = n.notifications.Notifications;
            l.Clear();

            n.ReplaceNotifications(l);
        }
    }
}
