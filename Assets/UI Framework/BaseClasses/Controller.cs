using System;
using Assets.Core;
using UnityEngine;

public abstract class Controller : BaseClass
{
    private View[] Views;
    private ButtonView[] buttonViews;

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

    public void Render()
    {
        foreach (var view in Views)
        {
            var startTime = DateTime.Now;

            // if (view.gameObject.activeSelf)
            // {
                view.ViewRender();
                
                Companies.Measure(view.name, startTime, MyProfiler, "Views");
            // }

            if (view.name == "Gameplay")
            {
                Debug.Log("Rendering Gameplay object");
            }
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

        // foreach (var view in GetComponents<View>())
        //     view.ViewRender();
        //
        // foreach (var view in GetComponents<ButtonView>())
        //     view.ViewRender();
    }

    private void FillListeners()
    {
        Views = GetComponents<View>();
        buttonViews = GetComponents<ButtonView>();
    }

    void OnEnable()
    {
        AttachListeners();

        FillListeners();

        Render();
    }

    void OnDisable()
    {
        DetachListeners();
    }

    //void OnDestroy()
    //{
    //    DetachListeners();
    //}

    public abstract void AttachListeners();
    public abstract void DetachListeners();
}