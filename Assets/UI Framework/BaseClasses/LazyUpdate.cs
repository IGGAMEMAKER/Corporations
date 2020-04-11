using System.Collections.Generic;

public class LazyUpdate : Controller
    , IDateListener
    , IMenuListener
    , INavigationHistoryListener
{
    public bool DateChanges = true;
    public bool MenuChanges = true;
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

    void IDateListener.OnDate(GameEntity entity, int date, int speed)
    {
        Render();
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        Render();
    }

    void INavigationHistoryListener.OnNavigationHistory(GameEntity entity, List<MenuComponent> queries)
    {
        Render();
    }
}
