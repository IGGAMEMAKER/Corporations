public class ListenCrunchChanges : Controller
    , ICrunchingListener
{
    public override void AttachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.AddCrunchingListener(this);
    }

    public override void DetachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.RemoveCrunchingListener(this);
    }

    void ICrunchingListener.OnCrunching(GameEntity entity)
    {
        Render();
    }
}
