using Assets.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FeatureView : View, IPointerEnterHandler, IPointerExitHandler
{
    public Text Name;
    public Text Benefits;
    public Text Rating;
    public Text UpgradeCost;

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

        Draw(PendingTaskIcon, Products.IsPendingFeature(product, featureName));

        RenderFeatureRating(product, featureName);

        RenderFeatureBenefit(featureName);

        RenderFeatureUpgradeProgress(product, featureName);

        RenderFeatureTypeIcon();
        
        RenderFeatureUpgradeCost(product, new TeamTaskFeatureUpgrade(NewProductFeature));
    }

    void RenderFeatureUpgradeCost(GameEntity company, TeamTask task)
    {
        if (UpgradeCost != null)
            UpgradeCost.text = Teams.GetFeatureUpgradeCost(company, task).ToString();
    }

    void RenderFeatureRating(GameEntity product, string featureName)
    {
        var upgraded = Products.IsUpgradedFeature(product, featureName);
        var rating = (int)Products.GetFeatureRating(product, featureName);
        
        Draw(Rating, true);

        Rating.text = rating.ToString("0LV");
        Rating.color = upgraded ? 
            Visuals.GetGradientColor(0, 10f, rating)
            :
            Visuals.GetColorFromString(Colors.COLOR_POSITIVE);        
    }

    void RenderFeatureBenefit(string featureName)
    {
        if (NewProductFeature.FeatureBonus.isMonetisationFeature)
            Benefits.text = "Income: " + Visuals.Positive(NewProductFeature.FeatureBonus.Max + "%");

        if (NewProductFeature.FeatureBonus.isRetentionFeature)
            Benefits.text = Visuals.Positive("Increases loyalty");
        

        var featureBenefit = NewProductFeature.IsMonetizationFeature
            ? Visuals.Positive("Increases income")
            :
            Visuals.Positive("Increases loyalty of users");
        GetComponent<Hint>().SetHint($"<size=30>{featureName}</size>\n\n{featureBenefit}");
    }

    void RenderFeatureUpgradeProgress(GameEntity product, string featureName)
    {
        if (Products.IsUpgradingFeature(product, featureName))
        {
            var task = Products.GetTeamTaskByFeatureName(product, featureName);
            
            ProgressBar.SetDescription("Upgrading feature");
            ProgressBar.SetValue(CurrentIntDate - task.StartDate, task.EndDate - task.StartDate);

            Show(ProgressBar);
        }
        else
        {
            Hide(ProgressBar);
        }

        ProgressImage.fillAmount = 0f;
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
