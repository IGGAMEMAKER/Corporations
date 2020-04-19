using System.Collections.Generic;

public class LazyUpdate : Controller
    , IAnyDateListener
    , IMenuListener
    , INavigationHistoryListener
{
    [UnityEngine.Header("Everyday changes")]
    public bool DateChanges = true;

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

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
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
