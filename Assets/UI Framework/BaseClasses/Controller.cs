using Assets.Core;

public abstract class Controller : BaseClass
{
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
