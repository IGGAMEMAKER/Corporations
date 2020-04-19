using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        public static long GetUpgradeCost(GameEntity product, GameContext gameContext, ProductUpgrade upgrade)
        {
            switch (upgrade)
            {
                case ProductUpgrade.BrandCampaign: return Marketing.GetBrandingCost(product, gameContext);
                case ProductUpgrade.TargetingCampaign: return Marketing.GetTargetingCost(product, gameContext);
            }

            return 0;
        }

        public static long GetUpgradeWorkerCost(GameEntity product, GameContext gameContext, ProductUpgrade upgrade)
        {
            switch (upgrade)
            {
                case ProductUpgrade.BrandCampaign: return Marketing.GetBrandingCost(product, gameContext);
                case ProductUpgrade.TargetingCampaign: return Marketing.GetTargetingCost(product, gameContext);
            }

            return 0;
        }


        // Concept upgrade
        public static void UpgradeProductLevel(GameEntity product, GameContext gameContext)
        {
            var revolutionChance = Products.GetInnovationChance(product, gameContext);

            var revolutionOccured = Random.Range(0, 100) < revolutionChance;

            var upgrade = 1;

            if (revolutionOccured && Products.IsWillInnovate(product, gameContext))
                upgrade = 2;

            product.ReplaceProduct(product.product.Niche, Products.GetProductLevel(product) + upgrade);
        }

        public static void ForceUpgrade(GameEntity product, GameContext gameContext, int upgrade)
        {
            product.ReplaceProduct(product.product.Niche, Products.GetProductLevel(product) + upgrade);

            Markets.UpdateMarketRequirements(product, gameContext);
        }
    }
}
