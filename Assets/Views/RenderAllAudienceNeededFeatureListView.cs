using Assets.Core;
using UnityEngine;

public class RenderAllAudienceNeededFeatureListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var features = Products.GetProductFeaturesList(Flagship, Q);

        SetItems(features);
    }
}
