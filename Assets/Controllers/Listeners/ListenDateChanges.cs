using UnityEngine;

public class ListenDateChanges : Controller
    , IAnyDateListener
{
    public override void AttachListeners()
    {
        ListenDateChanges(this);
    }

    public override void DetachListeners()
    {
        UnListenDateChanges(this);
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
