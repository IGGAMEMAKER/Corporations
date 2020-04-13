using System.Collections.Generic;

public class MonthlyUpdate : Controller
    , IDateListener
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

    void IDateListener.OnDate(GameEntity entity, int date)
    {
        if (date % 30 == 0)
            Render();
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        Render();
    }
}
