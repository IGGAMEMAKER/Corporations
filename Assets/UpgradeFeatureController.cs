using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFeatureController : ButtonController
{
    NewProductFeature feature;
    public override void Execute()
    {
        var product = Flagship;

        var rating = Products.GetFeatureRating(product, feature.Name);

        if (Products.IsCanUpgradeFeatures(product))
            Products.ForceUpgradeFeature(product, feature.Name, rating + 1);
    }

    internal void SetEntity(NewProductFeature feature)
    {
        this.feature = feature;
    }
}
