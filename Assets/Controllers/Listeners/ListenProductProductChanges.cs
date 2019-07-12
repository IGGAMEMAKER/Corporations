using System.Collections.Generic;

public class ListenProductProductChanges : Controller
    , IProductListener
{
    public override void AttachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.AddProductListener(this);
    }

    public override void DetachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.RemoveProductListener(this);
    }

    void IProductListener.OnProduct(GameEntity entity, int id, NicheType niche, int concept)
    {
        Render();
    }
}
