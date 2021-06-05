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

    internal void SetEntity(NewProductFeature newProductFeature)
    {
        Feature = newProductFeature;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var competitors = Companies.GetDirectCompetitors(Flagship, Q, true);
        var upgrades = competitors.Select(c => Random.Range(0, 10));

        FeatureName.text = Feature.Name;
        Upgrades.text = string.Join("   |   ", upgrades);
    }
}
