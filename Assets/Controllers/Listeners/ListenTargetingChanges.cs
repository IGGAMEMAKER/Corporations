public class ListenTargetingChanges : Controller
    , ITargetingListener
{
    public override void AttachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.AddTargetingListener(this);
    }

    public override void DetachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.RemoveTargetingListener(this);
    }

    void ITargetingListener.OnTargeting(GameEntity entity)
    {
        Render();
    }
}
