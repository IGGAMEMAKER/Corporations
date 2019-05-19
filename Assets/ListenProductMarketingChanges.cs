using System.Collections.Generic;

public class ListenProductMarketingChanges : Controller
    , IMarketingListener
{
    public override void AttachListeners()
    {
        if (MyProductEntity != null)
            MyProductEntity.AddMarketingListener(this);
    }

    public override void DetachListeners()
    {
        if (MyProductEntity != null)
            MyProductEntity.RemoveMarketingListener(this);
    }

    void IMarketingListener.OnMarketing(GameEntity entity, long brandPower, Dictionary<UserType, long> segments)
    {
        Render();
    }
}
