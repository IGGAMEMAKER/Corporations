using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SegmentListView : ListView
    , IMarketingListener
    , IProductListener
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

        int companyId = MyProductEntity.company.Id;

        var c = CompanyUtils.GetCompanyById(GameContext, companyId);

        SetItems(c.marketing.Segments.ToArray());
    }

    void Start()
    {
        Debug.Log("Start SegListView");

        MyProductEntity.AddMarketingListener(this);
        MyProductEntity.AddProductListener(this);
    }

    void OnEnable()
    {
        Render();
    }

    void IMarketingListener.OnMarketing(GameEntity entity, long clients, long brandPower, bool isTargetingEnabled, Dictionary<UserType, long> segments)
    {
        Render();
    }

    void IProductListener.OnProduct(GameEntity entity, int id, string name, NicheType niche, int productLevel, int improvementPoints, Dictionary<UserType, int> segments)
    {
        Render();
    }
}
