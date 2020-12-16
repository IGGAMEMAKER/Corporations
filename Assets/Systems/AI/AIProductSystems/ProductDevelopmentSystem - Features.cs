using System;
using Assets.Core;
using System.Linq;

public partial class ProductDevelopmentSystem
{
    void Monetize(GameEntity product)
    {
        var remainingFeatures = Products.GetUpgradeableMonetizationFeatures(product);

        foreach (var feature in remainingFeatures)
        {
            TryAddTask(product, new TeamTaskFeatureUpgrade(feature));

            Companies.LogSuccess(product, $"Added {feature.Name} for profit");
        }
    }

    void DeMonetize(GameEntity product)
    {
        var remainingFeatures = Products.GetMonetisationFeatures(product);

        foreach (var f in remainingFeatures)
        {
            Products.RemoveFeature(product, f.Name, gameContext);
        }
    }

    void ManageFeatures(GameEntity product)
    {
        var features = Products.GetUpgradeableRetentionFeatures(product);

        foreach (var f in features)
        {
            TryAddTask(product, new TeamTaskFeatureUpgrade(f));
        }
        

        // --------------

        // if (!features.Any())
        // {
        //     // upgrade corporate culture? feature CAP
        //     // hire more teams for SLOTS
        //     Companies.IncrementCorporatePolicy(gameContext, product, CorporatePolicy.DecisionsManagerOrTeam);
        // }
    }
}
