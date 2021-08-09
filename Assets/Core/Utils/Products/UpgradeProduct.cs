using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        public static TeamResource GetFeatureUpgradeCost() => new TeamResource(C.ITERATION_PROGRESS, 0, 0, 0, 0);

        public static bool IsCanUpgradeFeatures(GameEntity product) => Companies.IsEnoughResources(product, GetFeatureUpgradeCost());
        public static bool IsCanUpgradeFeature(GameEntity product, int index) => GetFeatureRating(product, index) < 10 && Companies.IsEnoughResources(product, GetFeatureUpgradeCost());

        public static void TryToUpgradeFeature(GameEntity product, NewProductFeature feature, GameContext gameContext) => TryToUpgradeFeature(product, feature.Name, gameContext);
        public static void TryToUpgradeFeature(GameEntity product, string featureName, GameContext gameContext)
        {
            var rating = GetFeatureRating(product, featureName);
            var nextRating = Mathf.Clamp(rating + 1, 0, 10);

            if (IsCanUpgradeFeatures(product))
            {
                ForceUpgradeFeature(product, featureName, nextRating, gameContext);
            }
        }

        public static void ForceUpgradeFeature(GameEntity product, string featureName, float value, GameContext gameContext)
        {
            product.features.Upgrades[featureName] = value;

            Companies.Pay(product, GetFeatureUpgradeCost(), "Feature");

            // Update Market Requirements AndNotifyAllProductsAboutChanges
            NotifyAllProductsAboutMarketRequirementsChanges(product, featureName, value, gameContext);
        }

        public static void NotifyAllProductsAboutMarketRequirementsChanges(GameEntity product, string featureName, float value, GameContext gameContext)
        {
            var niche = Markets.Get(gameContext, product);

            // Calculate changes in market
            Markets.GetMarketRequirements(gameContext, niche);

            // Notify
            var copy = Markets.CopyMarketRequirements(niche.marketRequirements.Features);
            foreach (var c in Companies.GetDirectCompetitors(product, gameContext, true))
            {
                c.ReplaceMarketRequirements(copy);
            }
        }


        public static void RemoveFeature(GameEntity product, string featureName, GameContext gameContext)
        {
            if (product.features.Upgrades.ContainsKey(featureName))
                product.features.Upgrades.Remove(featureName);
        }

        public static NewProductFeature GetBestFeatureUpgradePossibility(GameEntity product, GameContext gameContext)
        {
            var marketRequirements = Markets.GetMarketRequirementsForCompany(gameContext, product);

            var features = GetAllFeaturesForProduct();

            var sortedByImportance = features
                .OrderBy(f =>
                {
                    if (f.IsMonetizationFeature)
                        return 11;

                    var rating = GetFeatureRating(product, f.Name);
                    var max = marketRequirements.Features[f.ID];

                    if (rating >= 10 || rating == max)
                        return 110;

                    var rand = Random.Range(0, 1.5f);

                    return rand + max - rating;
                });

            return sortedByImportance.FirstOrDefault();
        }
    }
}
