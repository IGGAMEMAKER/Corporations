using Assets.Core;

public abstract class Controller : BaseClass
{
    public void ListenNavigationChanges(INavigationHistoryListener navigationHistoryListener)
    {
        ScreenUtils.GetMenu(Q).AddNavigationHistoryListener(navigationHistoryListener);
    }

    public void UnListenNavigationChanges(INavigationHistoryListener navigationHistoryListener)
    {
        ScreenUtils.GetMenu(Q).RemoveNavigationHistoryListener(navigationHistoryListener);
    }


    public void ListenMenuChanges(IMenuListener menuListener)
    {
        ScreenUtils.GetMenu(Q).AddMenuListener(menuListener);
    }

    public void UnListenMenuChanges(IMenuListener menuListener)
    {
        ScreenUtils.GetMenu(Q).RemoveMenuListener(menuListener);
    }


    public void ListenDateChanges(IAnyDateListener dateListener)
    {
        ScheduleUtils.ListenDateChanges(Q, dateListener);
    }

    public void UnListenDateChanges(IAnyDateListener dateListener)
    {
        ScheduleUtils.UnsubscribeFromDateChanges(Q, dateListener);
    }

    public GameEntity AnyChangeListener()
    {
        return ScreenUtils.GetMenu(Q);
    }

    public void Render()
    {
        foreach (var view in GetComponents<View>())
            view.ViewRender();

        foreach (var view in GetComponents<ToggleButtonController>())
            view.ViewRender();
    }

    void OnEnable()
    {
        AttachListeners();

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
