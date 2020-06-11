using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        var product = Flagship;

        var features = Products.GetAvailableFeaturesForProduct(product);

        var amountOfUpgradedFeatures = product.features.Upgrades.Count;

        var necessaryAmountOfFeatures = GetNecessaryAmountOfItems(amountOfUpgradedFeatures);

        SetItems(features.Take(necessaryAmountOfFeatures));
    }

    int GetNecessaryAmountOfItems(int openedAlready)
    {
        var necessaryAmountOfFeatures = 1;

        if (openedAlready == 0)
            necessaryAmountOfFeatures = 1;
        else if (openedAlready == 1)
            necessaryAmountOfFeatures = 2;
        else if (openedAlready == 2)
            necessaryAmountOfFeatures = 3;
        else
            necessaryAmountOfFeatures = openedAlready * 2;

        return necessaryAmountOfFeatures;
    }
}
