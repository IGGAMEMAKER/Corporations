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

    public void OnDate(GameEntity entity, int date)
    {
        Render();
    }
}
