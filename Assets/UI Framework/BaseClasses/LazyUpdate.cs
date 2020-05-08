using Assets.Core;
using System.Collections.Generic;

public class LazyUpdate : Controller
    , IDateListener
    , IMenuListener
    , INavigationHistoryListener
{
    [UnityEngine.Header("Everyday changes")]
    public bool DateChanges = true;

    [UnityEngine.Header("Periodical changes")]
    public bool OnPeriodChange = false;

    [UnityEngine.Header("Button clicks")]
    public bool MenuChanges = true;

    [UnityEngine.Header("Navigation")]
    public bool NavigationChanges = true;

    public override void AttachListeners()
    {
        if (DateChanges)
            ListenDateChanges(this);

        if (MenuChanges)
            ListenMenuChanges(this);

        if (!MenuChanges && NavigationChanges)
            ListenNavigationChanges(this);
    }

    public override void DetachListeners()
    {
        if (DateChanges)
            UnListenDateChanges(this);

        if (MenuChanges)
            UnListenMenuChanges(this);

        if (!MenuChanges && NavigationChanges)
            UnListenNavigationChanges(this);
    }

    public void OnDate(GameEntity entity, int date)
    {
        if (!OnPeriodChange || (OnPeriodChange && ScheduleUtils.IsPeriodEnd(Q)))
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
}
