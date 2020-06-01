using Entitas;
using System;
using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class NotificationUtils
    {
        private static GameEntity GetNotificationsComponent(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Notifications)[0];
        }

        public static List<NotificationMessage> GetNotifications(GameContext gameContext)
        {
            return GetNotificationsComponent(gameContext).notifications.Notifications;
        }

        public static void Subscribe(GameContext gameContext, IAnyNotificationsListener listener)
        {
            var c = GetNotificationsComponent(gameContext);

            c.AddAnyNotificationsListener(listener);
        }

        public static void UnSubscribe(GameContext gameContext, IAnyNotificationsListener listener)
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

        public static void ClearNotification(GameContext gameContext, int notificationId)
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

        //

        public static GameEntity GetGameEventContainerEntity(GameContext gameContext)
        {
            return GetNotificationsComponent(gameContext);
        }

        public static GameEventContainerComponent GetGameEventContainer(GameContext gameContext)
        {
            return GetGameEventContainerEntity(gameContext).gameEventContainer;
        }

        public static void AddGameEvent(GameContext gameContext, GameEvent gameEvent)
        {
            var container = GetGameEventContainerEntity(gameContext);
            var events = container.gameEventContainer.Events;
            
            events.Add(gameEvent);
            container.ReplaceGameEventContainer(events);
        }

        public static bool HasGameEvent(GameContext gameContext, GameEventType eventType)
        {
            return GetGameEventContainer(gameContext).Events.Find(g => g.eventType == GameEventType.NewMarketingChannel) != null;
        }
    }
}
