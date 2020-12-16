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
    [UnityEngine.Header("Everyday changes")]
    public bool DateChanges = true;

    [UnityEngine.Header("Periodical changes")]
    public bool OnPeriodChange = false;

    [UnityEngine.Header("Button clicks")]
    public bool MenuChanges = true;

    [UnityEngine.Header("Navigation")]
    public bool NavigationChanges = true;

    [UnityEngine.Header("GameEvents")]
    public bool ListenGameEvents = false;

    [UnityEngine.Header("Pause")]
    public bool ListenPauseEvents = false;

    [UnityEngine.Header("Profile stuff")]
    public bool PrintDailyMeasurements = false;

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

        if (PrintDailyMeasurements)
        {
            ListenDateChanges(this);
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
        
        if (PrintDailyMeasurements)
        {
            UnListenDateChanges(this);
        }
    }

    public void OnDate(GameEntity entity, int date)
    {
        if (!OnPeriodChange || (OnPeriodChange && ScheduleUtils.IsPeriodEnd(date)))
            Render();

        if (PrintDailyMeasurements)
        {
            return;
            // if (MyProfiler.Length > 0)
            // {
            //     bool isPeriodEnd = ScheduleUtils.IsPeriodEnd(date);
            //     bool isMonthEnd = ScheduleUtils.IsMonthEnd(date);
            //
            //     var prefix = "";
            //
            //     prefix += "Total: " + ProfilerMilliseconds + "ms ";
            //     
            //     if (isMonthEnd)
            //     {
            //         prefix += "<b>MONTH</b>: ";
            //     }
            //
            //     if (isPeriodEnd)
            //     {
            //         prefix += "<b>PERIOD</b>: ";
            //     }
            //
            //     Debug.Log(prefix + MyProfiler.ToString());
            //     ProfilerMilliseconds = 0;
            //     MyProfiler.Clear();
            // }
        }
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
