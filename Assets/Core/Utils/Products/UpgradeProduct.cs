using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        public static int GetFreeImprovements(GameEntity product)
        {
            return product.expertise.ExpertiseLevel;
        }

        public static bool HasFreeImprovements(GameEntity product)
        {
            return GetFreeImprovements(product) > 0;
        }

        // TODO move to separate file/delete
        public static void UpgradeFeatures(ProductFeature improvement, GameEntity product, GameContext gameContext)
        {
            var task = new CompanyTaskUpgradeFeature(product.company.Id, improvement);

            if (CanUpgradeFeature(improvement, product, gameContext, task))
            {
                product.expertise.ExpertiseLevel--;
                Cooldowns.AddCooldown(gameContext, task, 8);
            }
        }

        public static bool CanUpgradeFeature(ProductFeature improvement, GameEntity product, GameContext gameContext, CompanyTask task)
        {
            return HasFreeImprovements(product) && !Cooldowns.HasCooldown(gameContext, task);
        }

        ///
        public static long GetUpgradeCost(GameEntity product, GameContext gameContext, ProductUpgrade upgrade)
        {
            switch (upgrade)
            {
                case ProductUpgrade.BrandCampaign: return Marketing.GetBrandingCost(product, gameContext);
                case ProductUpgrade.Targeting: return Marketing.GetTargetingCost(product, gameContext);
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
