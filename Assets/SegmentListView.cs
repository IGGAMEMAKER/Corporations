using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SegmentListView : ListView
    , IMarketingListener
    , IProductListener
    , IFinanceListener
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = (KeyValuePair<UserType, long>)(object)entity;

        t.GetComponent<ClientSegmentView>()
            .Render(e.Key, MyProductEntity.company.Id);
    }

    void Render()
    {
        if (MyProductEntity == null)
            return;

        SetItems(MyProductEntity.marketing.Segments.ToArray());
    }

    void Start()
    {
        MyProductEntity.AddMarketingListener(this);
        MyProductEntity.AddProductListener(this);
        MyProductEntity.AddFinanceListener(this);
    }

    void OnEnable()
    {
        Render();
    }

    void IMarketingListener.OnMarketing(GameEntity entity, long brandPower, Dictionary<UserType, long> segments)
    {
        Render();
    }

    void IProductListener.OnProduct(GameEntity entity, int id, string name, NicheType niche, int productLevel, int improvementPoints, Dictionary<UserType, int> segments)
    {
        Render();
    }

    void IFinanceListener.OnFinance(GameEntity entity, Pricing price, MarketingFinancing marketingFinancing, int salaries, float basePrice)
    {
        Render();
    }
}
