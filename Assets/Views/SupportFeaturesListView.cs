using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportFeaturesListView : ListView
{
    public bool MarketingSupport = true;
    public bool Servers = false;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<SupportView>().SetEntity(entity as SupportFeature);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (MarketingSupport)
            SetItems(Products.GetMarketingSupportFeatures(Flagship));

        if (Servers)
            SetItems(Products.GetHighloadFeatures(Flagship));
    }
}
