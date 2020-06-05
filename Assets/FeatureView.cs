using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatureView : View
{
    public Text Name;
    public Text Benefits;
    public Text Rating;

    public NewProductFeature NewProductFeature;

    public override void ViewRender()
    {
        base.ViewRender();

        if (NewProductFeature == null)
            return;

        Name.text = NewProductFeature.Name;
        Benefits.text = GetFeatureBenefits();

        var rating = Random.Range(0, 10f);

        Rating.text = rating.ToString("0.0");
        Rating.color = Visuals.GetGradientColor(0, 10f, rating);
    }

    public string GetFeatureBenefits()
    {
        var b = NewProductFeature.FeatureBonus;

        if (b is FeatureBonusAcquisition)
            return $"+{b.Max}% user gain";

        if (b is FeatureBonusMonetisation)
            return $"+{b.Max}% income";

        if (b is FeatureBonusRetention)
            return $"-{b.Max}% client loss";

        return b.GetType().ToString();
    }

    public void SetFeature(NewProductFeature newProductFeature)
    {
        NewProductFeature = newProductFeature;

        ViewRender();
    }
}
