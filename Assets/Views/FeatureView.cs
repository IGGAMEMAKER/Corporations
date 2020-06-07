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

    public Image ProgressImage;

    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;

        if (NewProductFeature == null || product == null)
            return;

        var featureName = NewProductFeature.Name;

        Name.text = featureName;
        Benefits.text = GetFeatureBenefits();

        bool upgraded = Products.IsUpgradedFeature(product, featureName);
        var rating = Products.GetFeatureRating(product, featureName);

        Draw(Rating, upgraded);

        Rating.text = rating.ToString("0.0");
        Rating.color = Visuals.GetGradientColor(0, 10f, rating);

        bool hasCooldown = Cooldowns.HasCooldown(Q, $"company-{product.company.Id}-upgradeFeature-{featureName}", out SimpleCooldown cooldown);

        if (hasCooldown)
        {
            //var progress = (CurrentIntDate % C.PERIOD) / (float)C.PERIOD;
            var progress = CurrentIntDate - cooldown.StartDate;
            ProgressImage.fillAmount = (float)progress / (cooldown.EndDate - cooldown.StartDate);
        }
        else
        {
            ProgressImage.fillAmount = 0f;
        }
    }

    public string GetFeatureBenefits()
    {
        var b = NewProductFeature.FeatureBonus;

        if (b is FeatureBonusAcquisition)
            return $"+{b.Max}% user growth";

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
