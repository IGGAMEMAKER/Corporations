using Assets.Core;
using UnityEngine;

public class UpgradeableMonetizationFeaturesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature, null);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var features = Products.GetUpgradeableMonetizationFeatures(Flagship);

        SetItems(features);
    }

    private void OnEnable()
    {
        ViewRender();
    }
}
