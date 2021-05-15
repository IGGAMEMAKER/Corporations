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

    public void OnDate(GameEntity entity, int date)
    {
        if (date % 30 == 0)
            Render();
    }

    public void OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        Render();
    }
}
