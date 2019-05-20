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
        Debug.Log("Detach date listeners!");
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
