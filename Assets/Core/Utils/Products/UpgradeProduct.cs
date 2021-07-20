using System.Linq;

namespace Assets.Core
{
    public static partial class Products
    {
        public static TeamResource GetFeatureUpgradeCost() => new TeamResource(C.ITERATION_PROGRESS, 0, 0, 0, 0);

        public static bool IsCanUpgradeFeatures(GameEntity product) => Companies.IsEnoughResources(product, GetFeatureUpgradeCost());

        public static void TryToUpgradeFeature(GameEntity product, NewProductFeature feature, GameContext gameContext) => TryToUpgradeFeature(product, feature.Name, gameContext);
        public static void TryToUpgradeFeature(GameEntity product, string featureName, GameContext gameContext)
        {
            var rating = GetFeatureRating(product, featureName);

            if (IsCanUpgradeFeatures(product) && rating <= 9)
                ForceUpgradeFeature(product, featureName, rating + 1, gameContext);
        }

        public static void ForceUpgradeFeature(GameEntity product, string featureName, float value, GameContext gameContext)
        {
            product.features.Upgrades[featureName] = value;

            Companies.SpendResources(product, GetFeatureUpgradeCost(), "Feature");

            // Update Market Requirements AndNotifyAllProductsAboutChanges
            NotifyAllProductsAboutChanges(product, featureName, value, gameContext);
        }

        public static void NotifyAllProductsAboutChanges(GameEntity product, string featureName, float value, GameContext gameContext)
        {
            var niche = Markets.Get(gameContext, product);
            var index = product.features.Upgrades.Keys.ToList().IndexOf(featureName);

            Markets.GetMarketRequirements(gameContext, niche);

            if (niche.marketRequirements.Features[index] < value)
            {
                // new leader!
                // update data
                niche.marketRequirements.Features[index] = value;

                // notify everyone about updates
                var competitors = Companies.GetDirectCompetitors(product, gameContext, true);

                foreach (var c in competitors)
                {
                    c.marketRequirements.Features = niche.marketRequirements.Features;
                }
            }
        }


        public static void RemoveFeature(GameEntity product, string featureName, GameContext gameContext)
        {
            if (product.features.Upgrades.ContainsKey(featureName))
                product.features.Upgrades.Remove(featureName);
        }
    }
}
