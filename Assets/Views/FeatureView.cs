using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public GameObject PendingTaskIcon;

    public Image BorderImage;

    public AudiencesOnMainScreenListView _AudiencesOnMainScreenListView;
    AudiencesOnMainScreenListView AudiencesOnMainScreenListView
    {
        get
        {
            if (_AudiencesOnMainScreenListView == null)
            {
                _AudiencesOnMainScreenListView = FindObjectOfType<AudiencesOnMainScreenListView>();
            }

            return _AudiencesOnMainScreenListView;
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;

        if (NewProductFeature == null || product == null)
            return;

        var featureName = NewProductFeature.Name;
        Name.text = featureName;


        bool upgraded = Products.IsUpgradedFeature(product, featureName);
        var rating = (int)Products.GetFeatureRating(product, featureName);

        Draw(PendingTaskIcon, Products.IsPendingFeature(product, featureName));

        Draw(Rating, true);
        //Draw(Rating, upgraded && rating > 0);

        Rating.text = rating.ToString("0LV");
        //Rating.text = upgraded ? rating.ToString("0.0") + " / 10" : "0";// GetFeatureBenefits(upgraded, product);
        Rating.color = upgraded ? Visuals.GetGradientColor(0, 10f, rating) : Visuals.GetColorFromString(Colors.COLOR_POSITIVE);

        if (NewProductFeature.FeatureBonus.isMonetisationFeature)
            Benefits.text = "Income: " + Visuals.Positive(NewProductFeature.FeatureBonus.Max + "%");

        if (NewProductFeature.FeatureBonus.isRetentionFeature)
            Benefits.text = Visuals.Positive("Increases loyalty");

        var cooldownName = $"company-{product.company.Id}-upgradeFeature-{featureName}";
        bool hasCooldown = Cooldowns.HasCooldown(Q, cooldownName, out SimpleCooldown cooldown);

        
        var featureBenefit = NewProductFeature.IsMonetizationFeature ? Visuals.Positive("Increases income") : Visuals.Positive("Increases loyalty of users");
        GetComponent<Hint>().SetHint($"<size=30>{featureName}</size>\n\n{featureBenefit}");

        if (Teams.IsUpgradingFeature(product, Q, featureName))
        {
            //var progress = (CurrentIntDate % C.PERIOD) / (float)C.PERIOD;
            var progress = CurrentIntDate - cooldown.StartDate;
            ProgressImage.fillAmount = 1f; // (float)progress / (cooldown.EndDate - cooldown.StartDate);

            ProgressBar.SetDescription("Upgrading feature");
            ProgressBar.SetValue(CurrentIntDate - cooldown.StartDate, cooldown.EndDate - cooldown.StartDate);

            Show(ProgressBar);
            //Draw(Rating, false);
        }
        else
        {
            //Show(Rating);
            ProgressImage.fillAmount = 0f;

            Hide(ProgressBar);
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

    public void SetFeature(NewProductFeature newProductFeature, AudiencesOnMainScreenListView audiencesOnMainScreenListView)
    {
        NewProductFeature = newProductFeature;
        _AudiencesOnMainScreenListView = audiencesOnMainScreenListView;

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
        if (AudiencesOnMainScreenListView != null)
            AudiencesOnMainScreenListView.ShowAudienceLoyaltyChangeOnFeatureUpgrade(NewProductFeature);

        BorderImage.color = Visuals.GetColorFromString(Colors.COLOR_GOLD);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (AudiencesOnMainScreenListView != null)
            AudiencesOnMainScreenListView.HideLoyaltyChanges();

        RenderNeutralBorders();
    }

    void RenderNeutralBorders()
    {
        BorderImage.color = Visuals.GetColorFromString(Colors.COLOR_NEUTRAL);
    }
}
