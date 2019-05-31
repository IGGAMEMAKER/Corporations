public class LazyUpdate : Controller
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
