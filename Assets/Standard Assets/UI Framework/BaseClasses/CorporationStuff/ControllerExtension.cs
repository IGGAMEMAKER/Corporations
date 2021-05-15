using System;
using System.Runtime.Remoting.Messaging;
using Assets.Core;
using UnityEngine;

public abstract partial class Controller : BaseClass
{
    private static ProfilingComponent _profiler;

    public static ProfilingComponent MyProfiler
    {
        get
        {
            if (_profiler == null)
                _profiler = Companies.GetProfilingComponent(Q);

            return _profiler;
        }
    }

    void RenderAndMeasure()
    {
        foreach (var view in Views)
        {
            var startTime = DateTime.Now;

            // if (view.gameObject.activeSelf)
            // {
            view.ViewRender();

            Companies.Measure(view.name, startTime, MyProfiler, "Views");
            // }
        }

        foreach (var view in buttonViews)
        {
            var startTime = DateTime.Now;

            // if (view.gameObject.activeSelf)
            // {
            view.ViewRender();

            Companies.Measure(view.name, startTime, MyProfiler, "Views");
            // }
        }
    }

    // listeners
    public void ListenNavigationChanges(INavigationHistoryListener listener)
    {
        ScreenUtils.GetMenu(Q).AddNavigationHistoryListener(listener);
    }

    public void UnListenNavigationChanges(INavigationHistoryListener listener)
    {
        ScreenUtils.GetMenu(Q).RemoveNavigationHistoryListener(listener);
    }


    public void ListenMenuChanges(IMenuListener listener)
    {
        ScreenUtils.GetMenu(Q).AddMenuListener(listener);
    }

    public void UnListenMenuChanges(IMenuListener listener)
    {
        ScreenUtils.GetMenu(Q).RemoveMenuListener(listener);
    }


    public void ListenDateChanges(IDateListener listener)
    {
        ScheduleUtils.ListenDateChanges(Q, listener);
    }

    public void UnListenDateChanges(IDateListener listener)
    {
        ScheduleUtils.UnsubscribeFromDateChanges(Q, listener);
    }

    public GameEntity AnyChangeListener()
    {
        return ScreenUtils.GetMenu(Q);
    }
}