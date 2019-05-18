using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingAudienceReachView : View
    , IFinanceListener
{
    void OnEnable()
    {
        if (MyProductEntity != null)
            MyProductEntity.AddFinanceListener(this);

        Render();
    }

    void Render()
    {
        if (MyProductEntity != null)
            GetComponent<ColoredValue>().UpdateValue(MarketingUtils.GetTargetingEffeciency(GameContext, MyProductEntity));
    }

    void IFinanceListener.OnFinance(GameEntity entity, Pricing price, MarketingFinancing marketingFinancing, int salaries, float basePrice)
    {
        Debug.Log("OnFinance");

        Render();
    }
}
