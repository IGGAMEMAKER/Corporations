using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenProductFinancingController : Controller
    , IFinanceListener
{
    public override void AttachListeners()
    {
        if (MyProductEntity != null)
            MyProductEntity.AddFinanceListener(this);
    }

    public override void DetachListeners()
    {
        if (MyProductEntity != null)
            MyProductEntity.RemoveFinanceListener(this);
    }

    void IFinanceListener.OnFinance(GameEntity entity, Pricing price, MarketingFinancing marketingFinancing, int salaries, float basePrice)
    {
        Render();
    }
}
