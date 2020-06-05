using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductFeaturesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var features = new NewProductFeature[] {
            new NewProductFeature { Name = "Monetisation", FeatureBonus = new FeatureBonusMonetisation(15) },
            new NewProductFeature { Name = "Retention", FeatureBonus = new FeatureBonusRetention(5) },
            new NewProductFeature { Name = "Start Page", FeatureBonus = new FeatureBonusAcquisition(5) },
        };

        SetItems(features);
    }
}
