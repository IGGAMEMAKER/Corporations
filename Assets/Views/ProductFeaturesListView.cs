using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ProductFeaturesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var product = company;

        var features = GetFeatures(); // Products.GetAvailableFeaturesForProduct(product);

        var amountOfUpgradedFeatures = product.features.Upgrades.Count;

        var necessaryAmountOfFeatures = GetNecessaryAmountOfItems(amountOfUpgradedFeatures);

        SetItems(features.Take(necessaryAmountOfFeatures));
    }

    public abstract NewProductFeature[] GetFeatures();
    public GameEntity company => Flagship;

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
