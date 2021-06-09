using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FeatureView2 : View
{
    public Text FeatureName;
    public Text Upgrades;

    NewProductFeature Feature;
    public UpgradeFeatureController UpgradeFeatureController;

    internal void SetEntity(NewProductFeature newProductFeature)
    {
        Feature = newProductFeature;

        var competitors = Companies.GetDirectCompetitors(Flagship, Q, true);
        var upgrades = competitors.Select(c => Visuals.Colorize(Products.GetFeatureRating(c, Feature.Name) + "", c.isFlagship ? Colors.COLOR_BEST : Colors.COLOR_NEUTRAL));

        var space = "      ";

        FeatureName.text = "Upgrade " + Feature.Name;
        Upgrades.text = string.Join($"{space}|{space}", upgrades);

        UpgradeFeatureController.SetEntity(Feature);
    }
}
