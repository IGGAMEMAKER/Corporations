using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        // direct money cost
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

        // salaries cost for maintenance
        public static long GetUpgradeWorkerCost(GameEntity product, GameContext gameContext, ProductUpgrade upgrade)
        {
            var workerSalary = C.SALARIES_PROGRAMMER;

            switch (upgrade)
            {
                case ProductUpgrade.BrandCampaign: return GetBrandingWorkerCost(product, gameContext) * workerSalary;
                case ProductUpgrade.BrandCampaign2: return GetBrandingWorkerCost(product, gameContext) * 2 * workerSalary;
                case ProductUpgrade.BrandCampaign3: return GetBrandingWorkerCost(product, gameContext) * 5 * workerSalary;

                case ProductUpgrade.TargetingCampaign: return GetTargetingWorkerCost(product, gameContext) * workerSalary;
                case ProductUpgrade.TargetingCampaign2: return GetTargetingWorkerCost(product, gameContext) * 2 * workerSalary;
                case ProductUpgrade.TargetingCampaign3: return GetTargetingWorkerCost(product, gameContext) * 5 * workerSalary;

                case ProductUpgrade.QA: return GetQAWorkerCost(gameContext, product, ProductUpgrade.QA) * workerSalary;
                case ProductUpgrade.QA2: return GetQAWorkerCost(gameContext, product, ProductUpgrade.QA2) * workerSalary;
                case ProductUpgrade.QA3: return GetQAWorkerCost(gameContext, product, ProductUpgrade.QA3) * workerSalary;

                case ProductUpgrade.Support: return GetSupportWorkerCost(product, gameContext, ProductUpgrade.Support) * workerSalary;
                case ProductUpgrade.Support2: return GetSupportWorkerCost(product, gameContext, ProductUpgrade.Support2) * workerSalary;
                case ProductUpgrade.Support3: return GetSupportWorkerCost(product, gameContext, ProductUpgrade.Support3) * workerSalary;
            }

            return 0;
        }

        // programmers
        public static int GetQAWorkerCost(GameContext gameContext, GameEntity product, ProductUpgrade upgrade)
        {
            var niche = Markets.Get(gameContext, product);
            var complexity = (int)niche.nicheBaseProfile.Profile.AppComplexity;
            var concept = Products.GetProductLevel(product);

            var baseValue = 1;

            if (upgrade == ProductUpgrade.QA3)
                baseValue = (int)Mathf.Pow(1 + complexity / 40f, concept);
            else if (upgrade == ProductUpgrade.QA2)
                baseValue = (int)Mathf.Pow(1 + complexity / 50f, concept);
            else if (upgrade == ProductUpgrade.QA)
                baseValue = (int)Mathf.Pow(1 + complexity / 100f, concept);

            // desktop only
            var platformMultiplier = 1;

            return baseValue * platformMultiplier;
        }



        public static int GetSupportWorkerCost(GameEntity e, GameContext gameContext, ProductUpgrade upgrade)
        {
            var clients = Marketing.GetClients(e);

            var baseValue = 1;

            if (upgrade == ProductUpgrade.Support3)
                baseValue = (int)Mathf.Pow(clients / 1000, 0.75f);
            else if (upgrade == ProductUpgrade.Support2)
                baseValue = (int)Mathf.Pow(clients / 1000, 0.5f);
            else if (upgrade == ProductUpgrade.Support)
                baseValue = (int)Mathf.Pow(clients / 1000, 0.2f);

            var platformMultiplier = 1;

            return baseValue * platformMultiplier;
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
