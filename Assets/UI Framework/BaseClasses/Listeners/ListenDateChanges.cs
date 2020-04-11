using UnityEngine;

public class ListenDateChanges : Controller
    , IDateListener
{
    public override void AttachListeners()
    {
        ListenDateChanges(this);
    }

    public override void DetachListeners()
    {
        UnListenDateChanges(this);
    }

    void IDateListener.OnDate(GameEntity entity, int date, int speed)
    {
        Render();
    }
}
