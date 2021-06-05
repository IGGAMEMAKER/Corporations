using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeableFeaturesListView2 : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<FeatureView2>().SetEntity(entity as NewProductFeature);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var features = Products.GetUpgradeableRetentionFeatures(company);

        SetItems(features);
    }
}
