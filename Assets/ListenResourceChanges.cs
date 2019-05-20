using Assets.Classes;

public class ListenResourceChanges : Controller
    , ICompanyResourceListener
{
    public override void AttachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.AddCompanyResourceListener(this);
    }

    public override void DetachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.RemoveCompanyResourceListener(this);
    }

    void ICompanyResourceListener.OnCompanyResource(GameEntity entity, TeamResource resources)
    {
        Render();
    }
}
