using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        public static long GetUpgradeCost(GameEntity product, GameContext gameContext, ProductUpgrade upgrade)
        {
            var marketingLeadBonus = 100 - Teams.GetMarketingLeadBonus(product, gameContext);

            switch (upgrade)
            {
                case ProductUpgrade.BrandCampaign: return Marketing.GetBrandingCost(product, gameContext) * marketingLeadBonus / 100;
                case ProductUpgrade.BrandCampaign2: return Marketing.GetBrandingCost(product, gameContext) * 2 * marketingLeadBonus / 100;
                case ProductUpgrade.BrandCampaign3: return Marketing.GetBrandingCost(product, gameContext) * 10 * marketingLeadBonus / 100;

                case ProductUpgrade.TargetingCampaign: return Marketing.GetTargetingCost(product, gameContext) * marketingLeadBonus / 100;
                case ProductUpgrade.TargetingCampaign2: return Marketing.GetTargetingCost(product, gameContext) * 2 * marketingLeadBonus / 100;
                case ProductUpgrade.TargetingCampaign3: return Marketing.GetTargetingCost(product, gameContext) * 10 * marketingLeadBonus / 100;
            }

            return 0;
        }

        public static long GetUpgradeWorkerCost(GameEntity product, GameContext gameContext, ProductUpgrade upgrade)
        {
            switch (upgrade)
            {
                case ProductUpgrade.BrandCampaign: return GetBrandingWorkerCost(product, gameContext);
                case ProductUpgrade.BrandCampaign2: return GetBrandingWorkerCost(product, gameContext) * 2;
                case ProductUpgrade.BrandCampaign3: return GetBrandingWorkerCost(product, gameContext) * 5;

                case ProductUpgrade.TargetingCampaign: return GetTargetingWorkerCost(product, gameContext);
                case ProductUpgrade.TargetingCampaign2: return GetTargetingWorkerCost(product, gameContext) * 2;
                case ProductUpgrade.TargetingCampaign3: return GetTargetingWorkerCost(product, gameContext) * 5;
            }

            return 0;
        }

        public static int GetBrandingWorkerCost(GameEntity product, GameContext gameContext)
        {
            return 1;
        }

        public static int GetTargetingWorkerCost(GameEntity product, GameContext gameContext)
        {
            return 1;
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
