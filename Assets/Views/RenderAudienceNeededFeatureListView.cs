using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderAudienceNeededFeatureListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature);
    }

    internal void SetAudience(AudienceInfo info)
    {
        var company = Flagship;

        var features = Products.GetAllFeaturesForProduct(company);
        SetItems(features);
    }
}
