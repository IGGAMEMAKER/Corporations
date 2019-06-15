using Assets.Utils;

public abstract class Controller : BaseClass
{
    public void ListenMenuChanges(IMenuListener menuListener)
    {
        ScreenUtils.GetMenu(GameContext).AddMenuListener(menuListener);
    }

    public void UnListenMenuChanges(IMenuListener menuListener)
    {
        ScreenUtils.GetMenu(GameContext).RemoveMenuListener(menuListener);
    }


    public void ListenDateChanges(IAnyDateListener dateListener)
    {
        ScheduleUtils.ListenDateChanges(GameContext, dateListener);
    }

    public void UnListenDateChanges(IAnyDateListener dateListener)
    {
        ScheduleUtils.UnsubscribeFromDateChanges(GameContext, dateListener);
    }

    public GameEntity AnyChangeListener()
    {
        return ScreenUtils.GetMenu(GameContext);
    }

    public void Render()
    {
        foreach (var view in GetComponents<View>())
            view.ViewRender();
    }

    void OnEnable()
    {
        AttachListeners();

        Render();
    }

    private void OnDisable()
    {
        DetachListeners();
    }

    //private void OnDestroy()
    //{
    //    DetachListeners();
    //}

    public abstract void AttachListeners();
    public abstract void DetachListeners();
}
