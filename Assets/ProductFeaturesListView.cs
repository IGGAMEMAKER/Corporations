using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureBonus
{
    // max bonus
    public float Max;

    public FeatureBonus(float Max)
    {
        this.Max = Max;
    }
}

public class FeatureBonusAcquisition : FeatureBonus
{
    public FeatureBonusAcquisition(float Max) : base(Max)
    {
    }
}
public class FeatureBonusRetention : FeatureBonus
{
    public FeatureBonusRetention(float Max) : base(Max)
    {
    }
}
public class FeatureBonusMonetisation : FeatureBonus
{
    public FeatureBonusMonetisation(float Max) : base(Max)
    {
    }
}

public class NewProductFeature
{
    public string Name;
    public FeatureBonus FeatureBonus;
}

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
            new NewProductFeature { Name = "Monetisation", FeatureBonus = new FeatureBonusMonetisation(5) },
            new NewProductFeature { Name = "Retention", FeatureBonus = new FeatureBonusRetention(5) },
            new NewProductFeature { Name = "Start Page", FeatureBonus = new FeatureBonusAcquisition(5) },
        };

        SetItems(features);
    }
}
