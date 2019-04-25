using Assets.Utils;
using Entitas;
using System;
using System.Collections.Generic;

public class NotificationInitializerSystem : IInitializeSystem
{
    readonly GameContext GameContext;

    public NotificationInitializerSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        var e = GameContext.CreateEntity();

        var notifications = new List<NotificationMessage>();

        e.AddNotifications(notifications);
    }
}
