using Assets.Core;
using System.Linq;

public partial class ProductDevelopmentSystem
{
    void Monetize(GameEntity product)
    {
        var remainingFeatures = Products.GetUpgradeableMonetizationFeatures(product);
        var churn = Marketing.GetChurnRate(product, gameContext);

        if (remainingFeatures.Any() && churn < 4)
        {
            var feature = remainingFeatures.First();

            Products.TryToUpgradeFeature(product, feature, gameContext);

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
            Products.TryToUpgradeFeature(product, f, gameContext);
        }
    }
}
