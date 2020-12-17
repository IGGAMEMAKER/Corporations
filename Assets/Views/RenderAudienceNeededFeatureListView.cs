using Assets.Core;
using UnityEngine;

public class RenderAudienceNeededFeatureListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature, null);
    }

    internal void SetAudience(AudienceInfo info)
    {
        var company = Flagship;

        var features = Products.GetAllFeaturesForProduct(company);
        SetItems(features);
    }
}
