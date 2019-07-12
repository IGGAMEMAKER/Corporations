using System.Collections.Generic;

public class ListenProductMarketingChanges : Controller
    , IMarketingListener
{
    public override void AttachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.AddMarketingListener(this);
    }

    public override void DetachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.RemoveMarketingListener(this);
    }

    void IMarketingListener.OnMarketing(GameEntity entity, long brandPower, long segments)
    {
        Render();
    }
}
