public class ListenProductReleaseChanges : Controller
    , IReleaseListener
{
    public override void AttachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.AddReleaseListener(this);
    }

    public override void DetachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.RemoveReleaseListener(this);
    }

    void IReleaseListener.OnRelease(GameEntity entity)
    {
        Render();
    }
}
