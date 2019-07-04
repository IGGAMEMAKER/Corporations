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

        internal static void Subscribe(GameContext gameContext, IAnyNotificationsListener listener)
        {
            var c = GetNotificationsComponent(gameContext);

            c.AddAnyNotificationsListener(listener);
        }

        internal static void UnSubscribe(GameContext gameContext, IAnyNotificationsListener listener)
        {
            var c = GetNotificationsComponent(gameContext);

            c.RemoveAnyNotificationsListener(listener);
        }

        public static void AddNotification(GameContext gameContext, NotificationMessage notificationMessage)
        {
            var n = GetNotificationsComponent(gameContext);

            var notys = n.notifications.Notifications;
            notys.Add(notificationMessage);

            n.ReplaceNotifications(notys);
        }

        internal static void ClearNotification(GameContext gameContext, int notificationId)
        {
            var n = GetNotificationsComponent(gameContext);

            var l = n.notifications.Notifications;
            l.RemoveAt(notificationId);

            n.ReplaceNotifications(l);
        }

        public static void ClearNotifications(GameContext gameContext)
        {
            var n = GetNotificationsComponent(gameContext);

            var l = n.notifications.Notifications;
            l.Clear();

            n.ReplaceNotifications(l);
        }
    }
}
