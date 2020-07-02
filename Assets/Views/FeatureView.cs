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

    public ProgressBar ProgressBar;
    public Image ProgressImage;

    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;

        if (NewProductFeature == null || product == null)
            return;

        var featureName = NewProductFeature.Name;

        bool upgraded = Products.IsUpgradedFeature(product, featureName);
        var rating = Products.GetFeatureRating(product, featureName);

        Name.text = featureName;
        Benefits.text = GetFeatureBenefits(upgraded, product);


        Draw(Rating, upgraded);

        Rating.text = rating.ToString("0.0");
        Rating.color = Visuals.GetGradientColor(0, 10f, rating);

        var cooldownName = $"company-{product.company.Id}-upgradeFeature-{featureName}";
        bool hasCooldown = Cooldowns.HasCooldown(Q, cooldownName, out SimpleCooldown cooldown);

        if (Products.IsUpgradingFeature(product, Q, featureName))
        {
            //var progress = (CurrentIntDate % C.PERIOD) / (float)C.PERIOD;
            var progress = CurrentIntDate - cooldown.StartDate;
            ProgressImage.fillAmount = 1f; // (float)progress / (cooldown.EndDate - cooldown.StartDate);

            Draw(ProgressBar, true);
            ProgressBar.SetDescription("Upgrading feature");
            ProgressBar.SetValue(CurrentIntDate - cooldown.StartDate, cooldown.EndDate - cooldown.StartDate);

            Draw(Rating, false);
        }
        else
        {
            Draw(Rating, true);
            ProgressImage.fillAmount = 0f;
            Draw(ProgressBar, false);
        }

        ProgressImage.fillAmount = 0f;
    }

    public string GetFeatureBenefits(bool isUpgraded, GameEntity product)
    {
        var b = NewProductFeature.FeatureBonus;

        var benefit = isUpgraded ?
            Products.GetFeatureActualBenefit(product, NewProductFeature)
            :
            Products.GetFeatureMaxBenefit(product, NewProductFeature);

        var benefitFormatted = benefit.ToString("0.0");
        if (b is FeatureBonusAcquisition)
            return $"+{benefitFormatted}%";
            //return $"+{benefitFormatted}% user growth";

        if (b is FeatureBonusMonetisation)
            return $"+{benefitFormatted}%";
            //return $"+{benefitFormatted}% income";

        if (b is FeatureBonusRetention)
            return $"-{benefitFormatted}%";
            //return $"-{benefitFormatted}% client loss";

        return b.GetType().ToString();
    }

    public void SetFeature(NewProductFeature newProductFeature)
    {
        NewProductFeature = newProductFeature;

        ViewRender();
    }
}
