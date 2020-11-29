using Assets.Core;
using System.Linq;

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    void Monetise(GameEntity product)
    {
        var remainingFeatures = Products.GetUpgradeableMonetizationFeatures(product);

        var segments = Marketing.GetAudienceInfos();

        foreach (var feature in remainingFeatures)
        {
            TryAddTask(product, new TeamTaskFeatureUpgrade(feature));

            Companies.LogSuccess(product, $"Added {feature.Name} for profit");
        }
    }

    void DeMonetise(GameEntity product)
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

        if (features.Count() == 0)
        {
            // upgrade corporate culture? feature CAP
            // hire more teams for SLOTS
            Companies.IncrementCorporatePolicy(gameContext, product, CorporatePolicy.DecisionsManagerOrTeam);

            return;
        }
    }
}
