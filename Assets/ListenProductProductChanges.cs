using System.Collections.Generic;

public class ListenProductProductChanges : Controller
    , IProductListener
{
    public override void AttachListeners()
    {
        if (MyProductEntity != null)
            MyProductEntity.AddProductListener(this);
    }

    public override void DetachListeners()
    {
        if (MyProductEntity != null)
            MyProductEntity.RemoveProductListener(this);
    }

    void IProductListener.OnProduct(GameEntity entity, int id, string name, NicheType niche, int productLevel, int improvementPoints, Dictionary<UserType, int> segments)
    {
        Render();
    }
}
