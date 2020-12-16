using System;
using System.Collections.Generic;
using System.Text;
using Assets.Core;
using UnityEngine;

public abstract class Controller : BaseClass
{
    private View[] Views;
    private ButtonView[] buttonViews;

    private static StringBuilder _profiler;
    public long profilerMilliseconds;

    public static StringBuilder MyProfiler
    {
        get
        {
            if (_profiler == null)
            {
                _profiler = new StringBuilder();
            }

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
        var starts = new Dictionary<string, DateTime>();
        var ends = new Dictionary<string, DateTime>();
        
        foreach (var view in Views)
        {
            starts[view.name] = DateTime.Now;
            var startTime = DateTime.Now;
            
            if (view.gameObject.activeSelf)
            {
                view.ViewRender();
            }

            var endTime = DateTime.Now;
            ends[view.name] = DateTime.Now;
            
            var diff = endTime - startTime;
            var duration = diff.Milliseconds;

            profilerMilliseconds += duration;
            
            if (duration > 0)
                MyProfiler.AppendLine($@"{view.name}: {duration}ms");
        }

        // foreach (var pair in starts)
        // {
        //     var key = pair.Key;
        //
        //     var diff = ends[key] - pair.Value;
        //     var duration = diff.Milliseconds; 
        //     // ends[key].Millisecond - pair.Value.Millisecond;
        //     
        //     if (duration > 0)
        //         MyProfiler.AppendLine($@"{key}: {duration}ms");
        // }
        
        foreach (var view in buttonViews)
        {
            if (view.gameObject.activeSelf)
                view.ViewRender();
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
