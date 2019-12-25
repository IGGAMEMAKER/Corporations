using System.Collections.Generic;

public class MonthlyUpdate : Controller
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

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date, int speed)
    {
        if (date % 30 == 0)
            Render();
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        Render();
    }
}
