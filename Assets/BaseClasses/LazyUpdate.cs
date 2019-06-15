using System.Collections.Generic;

public class LazyUpdate : Controller
    , IAnyDateListener
    , IMenuListener
{
    public override void AttachListeners()
    {
        ListenDateChanges(this);
        ListenMenuChanges(this);
    }

    public override void DetachListeners()
    {
        UnListenDateChanges(this);
        UnListenMenuChanges(this);
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        Render();
    }
}
