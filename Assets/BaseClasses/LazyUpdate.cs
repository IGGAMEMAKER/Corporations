using System.Collections.Generic;

public class LazyUpdate : Controller
    , IAnyDateListener
    , IMenuListener
{
    public bool DateChanges = true;
    public bool MenuChanges = true;

    public override void AttachListeners()
    {
        if (DateChanges)
            ListenDateChanges(this);

        if (MenuChanges)
            ListenMenuChanges(this);
    }

    public override void DetachListeners()
    {
        if (DateChanges)
            UnListenDateChanges(this);

        if (MenuChanges)
            UnListenMenuChanges(this);
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date, int speed)
    {
        Render();
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        Render();
    }
}
