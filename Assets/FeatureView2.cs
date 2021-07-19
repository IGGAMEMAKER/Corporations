using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FeatureView2 : View
{
    public Text FeatureName;
    public Text Rating;
    public Text Upgrades;

    public Text Leader;
    public Text You;
    public Text Benefit;

    public Text BenefitDescription;


    NewProductFeature Feature;
    public UpgradeFeatureController UpgradeFeatureController;

    internal void SetEntity(NewProductFeature newProductFeature)
    {
        Feature = newProductFeature;

        var competitors = Companies.GetDirectCompetitors(Flagship, Q, true);
        var upgrades = competitors.Select(c => Visuals.Colorize((int)Products.GetFeatureRating(c, Feature.Name) + "", c.isFlagship ? Colors.COLOR_BEST : Colors.COLOR_NEUTRAL));
        var maxLVL = competitors.Max(c => Products.GetFeatureRating(c, Feature.Name));

        var leader = competitors.First(c => Products.GetFeatureRating(c, Feature.Name) == maxLVL);
        var companyName = leader.company.Name;

        var space = "      ";

        var rating = Products.GetFeatureRating(Flagship, Feature.Name);

        var marketRequirements = Markets.GetMarketRequirementsForCompany(Q, Flagship);
        var featureIndex = Products.GetAllFeaturesForProduct().Select(f => f.Name).ToList().IndexOf(Feature.Name);

        FeatureName.text = Feature.Name;
        Rating.text = rating + "LVL";
        Rating.color = Visuals.GetGradientColor(0, marketRequirements.Features[featureIndex], rating);


        RenderFeatureBenefit(rating);

        
        Upgrades.text = string.Join($"{space}|{space}", upgrades);
        Hide(Upgrades);

        Leader.text = Visuals.Colorize(companyName, leader.isFlagship ? Colors.COLOR_BEST : Colors.COLOR_NEUTRAL) + "\n" + (int)maxLVL;
        You.text = "" + rating;
        Benefit.text = $"+{Visuals.Positive("5% growth")}";

        UpgradeFeatureController.SetEntity(Feature);
    }

    void RenderFeatureBenefit(float rating)
    {
        if (Feature.IsMonetizationFeature)
        {
            BenefitDescription.text = "+10% income";
        }

        if (Feature.IsRetentionFeature)
        {
            if (rating == 0)
                BenefitDescription.text = "+10% audience gain";
            else
                BenefitDescription.text = "-1% client loss";
        }
    }
}
