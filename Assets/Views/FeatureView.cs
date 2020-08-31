using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FeatureView : View, IPointerEnterHandler, IPointerExitHandler
{
    public Text Name;
    public Text Benefits;
    public Text Rating;

    public NewProductFeature NewProductFeature;

    public ProgressBar ProgressBar;
    public Image ProgressImage;

    public Image FeatureTypeIcon;

    public Sprite RetentionImage;
    public Sprite AcquisitionImage;
    public Sprite MonetisationImage;
    public Sprite UnknownImage;

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

        Draw(Rating, upgraded);

        Rating.text = upgraded ? rating.ToString("0.0") + " / 10" : "0";// GetFeatureBenefits(upgraded, product);
        Rating.color = upgraded ? Visuals.GetGradientColor(0, 10f, rating) : Visuals.GetColorFromString(Colors.COLOR_POSITIVE);

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

            //Draw(Rating, false);
        }
        else
        {
            Draw(Rating, true);
            ProgressImage.fillAmount = 0f;

            Draw(ProgressBar, false);
        }

        ProgressImage.fillAmount = 0f;
        RenderFeatureTypeIcon();
    }

    float GetFeatureBenefit(bool isUpgraded, GameEntity product) => isUpgraded ?
            Products.GetFeatureActualBenefit(product, NewProductFeature)
            :
            Products.GetFeatureMaxBenefit(product, NewProductFeature);

    public string GetFeatureBenefits(bool isUpgraded, GameEntity product)
    {
        var b = NewProductFeature.FeatureBonus;

        var benefit = Mathf.Abs(GetFeatureBenefit(isUpgraded, product));

        var benefitFormatted = benefit.ToString("0.0");
        if (b.isAcquisitionFeature)
            return $"{benefitFormatted}";
            //return $"+{benefitFormatted}% user growth";

        if (b.isMonetisationFeature)
            return $"{benefitFormatted}";
            //return $"+{benefitFormatted}% income";

        if (b.isRetentionFeature)
            return $"{benefitFormatted}";
            //return $"-{benefitFormatted}% client loss";

        return b.GetType().ToString();
    }

    public void SetFeature(NewProductFeature newProductFeature)
    {
        NewProductFeature = newProductFeature;

        ViewRender();
    }

    void RenderFeatureTypeIcon()
    {
        if (NewProductFeature.FeatureBonus.isAcquisitionFeature)
            FeatureTypeIcon.sprite = AcquisitionImage;
        else if (NewProductFeature.FeatureBonus.isMonetisationFeature)
            FeatureTypeIcon.sprite = MonetisationImage;
        else if (NewProductFeature.FeatureBonus.isRetentionFeature)
            FeatureTypeIcon.sprite = RetentionImage;
        else
            FeatureTypeIcon.sprite = UnknownImage;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        var view = FindObjectOfType<AudiencesOnMainScreenListView>();
        
        if (view != null)
            view.ShowAudienceLoyaltyChangeOnFeatureUpgrade(NewProductFeature);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        var view = FindObjectOfType<AudiencesOnMainScreenListView>();

        if (view != null)
            view.HideLoyaltyChanges();
    }
}
