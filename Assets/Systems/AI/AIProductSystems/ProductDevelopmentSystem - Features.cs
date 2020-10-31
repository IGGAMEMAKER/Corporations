using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    void Monetise(GameEntity product)
    {
        var remainingFeatures = Products.GetAllFeaturesForProduct(product).Where(f => !Products.IsUpgradingFeature(product, gameContext, f.Name) && f.FeatureBonus.isMonetisationFeature);

        var segments = Marketing.GetAudienceInfos();

        foreach (var feature in remainingFeatures)
        {
            bool willUpsetPeople = false;

            foreach (var s in segments)
            {
                var loyalty = Marketing.GetSegmentLoyalty(product, s.ID);
                var attitude = feature.AttitudeToFeature[s.ID];

                // will make audience sad
                if (loyalty + attitude < 0 && Marketing.IsImportantAudience(product, s.ID))
                {
                    Companies.Log(product, $"Wanted to add {feature.Name}, but this will dissapoint {s.Name}");
                    willUpsetPeople = true;

                    break;
                }
            }

            if (!willUpsetPeople)
            {
                TryAddTask(product, new TeamTaskFeatureUpgrade(feature));
                Companies.LogSuccess(product, $"Added {feature.Name} for profit");
            }
        }
    }

    void DeMonetise(GameEntity product)
    {
        var remainingFeatures = Products.GetAllFeaturesForProduct(product).Where(f => f.FeatureBonus.isMonetisationFeature);

        foreach (var f in remainingFeatures)
        {
            Products.RemoveFeature(product, f.Name, gameContext);
        }
    }

    void ManageFeatures(GameEntity product)
    {
        var remainingFeatures = Products.GetAllFeaturesForProduct(product).Where(f => !Products.IsUpgradingFeature(product, gameContext, f.Name));

        if (remainingFeatures.Count() == 0)
            return;

        var feature = remainingFeatures.First();
        if (feature.FeatureBonus.isMonetisationFeature) //  && feature.Name.Contains("Ads")
        {
            var segments = Marketing.GetAudienceInfos();

            foreach (var s in segments)
            {
                var loyalty = Marketing.GetSegmentLoyalty(product, s.ID);
                var attitude = feature.AttitudeToFeature[s.ID];

                // will make audience sad
                if (loyalty + attitude < 0 && Marketing.IsImportantAudience(product, s.ID))
                    return;
            }
        }

        TryAddTask(product, new TeamTaskFeatureUpgrade(feature));
    }
}
