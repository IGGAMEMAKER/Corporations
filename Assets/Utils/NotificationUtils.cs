using Entitas;
using System;
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

        internal static void SubscribeToChanges(GameContext gameContext, IAnyNotificationsListener listener)
        {
            var c = GetNotificationsComponent(gameContext);

            c.AddAnyNotificationsListener(listener);
        }

        public static void AddNotification(GameContext gameContext, NotificationMessage notificationMessage)
        {
            var n = GetNotificationsComponent(gameContext);

            var notys = n.notifications.Notifications;
            notys.Add(notificationMessage);

            n.ReplaceNotifications(notys);
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
