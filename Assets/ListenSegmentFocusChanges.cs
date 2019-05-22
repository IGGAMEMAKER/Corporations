public class ListenSegmentFocusChanges : Controller
    , ITargetUserTypeListener
{
    public override void AttachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.AddTargetUserTypeListener(this);
    }

    public override void DetachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.RemoveTargetUserTypeListener(this);
    }

    void ITargetUserTypeListener.OnTargetUserType(GameEntity entity, UserType userType)
    {
        Render();
    }
}
