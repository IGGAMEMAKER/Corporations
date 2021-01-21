using Assets.Core;
using System.Collections.Generic;
using UnityEngine;

public class LazyUpdate : Controller
    , IDateListener
    , IMenuListener
    , INavigationHistoryListener
    , IAnyGameEventContainerListener
    , IAnyTimerRunningListener
    , IAnyGamePausedListener
{
    [Header("Everyday changes")]
    public bool DateChanges = true;

    [Header("Periodical changes")]
    public bool OnPeriodChange = false;

    [Header("Button clicks")]
    public bool MenuChanges = true;

    [Header("Navigation")]
    public bool NavigationChanges = true;

    [Header("GameEvents")]
    public bool ListenGameEvents = false;

    [Header("Pause")]
    public bool ListenPauseEvents = false;

    public override void AttachListeners()
    {
        if (DateChanges)
            ListenDateChanges(this);

        if (MenuChanges)
            ListenMenuChanges(this);

        if (!MenuChanges && NavigationChanges)
            ListenNavigationChanges(this);

        if (ListenGameEvents)
            NotificationUtils.GetGameEventContainerEntity(Q).AddAnyGameEventContainerListener(this);

        if (ListenPauseEvents)
        {
            ScheduleUtils.GetDateContainer(Q).AddAnyTimerRunningListener(this);
            ScheduleUtils.GetDateContainer(Q).AddAnyGamePausedListener(this);
        }
    }

    public override void DetachListeners()
    {
        if (DateChanges)
            UnListenDateChanges(this);

        if (MenuChanges)
            UnListenMenuChanges(this);

        if (!MenuChanges && NavigationChanges)
            UnListenNavigationChanges(this);

        if (ListenGameEvents)
            NotificationUtils.GetGameEventContainerEntity(Q).RemoveAnyGameEventContainerListener(this);

        if (ListenPauseEvents)
        {
            ScheduleUtils.GetDateContainer(Q).RemoveAnyTimerRunningListener(this);
            ScheduleUtils.GetDateContainer(Q).RemoveAnyGamePausedListener(this);
        }
    }

    public void OnDate(GameEntity entity, int date)
    {
        if (!OnPeriodChange || (OnPeriodChange && ScheduleUtils.IsPeriodEnd(date)))
            Render();
    }

    public void OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        Render();
    }

    public void OnNavigationHistory(GameEntity entity, List<MenuComponent> queries)
    {
        Render();
    }

    void IAnyGameEventContainerListener.OnAnyGameEventContainer(GameEntity entity, List<GameEvent> events)
    {
        Render();
    }

    void IAnyGamePausedListener.OnAnyGamePaused(GameEntity entity)
    {
        Render();
    }

    void IAnyTimerRunningListener.OnAnyTimerRunning(GameEntity entity)
    {
        Render();
    }
}
