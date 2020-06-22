using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportFeaturesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<SupportView>().SetEntity(entity as SupportFeature);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var features = Products.GetAvailableSupportFeaturesForProduct(Flagship);

        SetItems(features);
    }
}
