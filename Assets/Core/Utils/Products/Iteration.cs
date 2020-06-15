using System;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        public static int GetBaseIterationTime(GameContext gameContext, GameEntity company) => GetBaseIterationTime(Markets.Get(gameContext, company));
        public static int GetBaseIterationTime(GameEntity niche) => GetBaseIterationTime(niche.nicheBaseProfile.Profile.NicheSpeed);
        public static int GetBaseIterationTime(NicheSpeed nicheChangeSpeed)
        {
            return 12;
        }

        public static void UpgradeFeature(GameEntity product, string featureName, GameContext gameContext)
        {
            if (IsUpgradedFeature(product, featureName))
            {
                // 0.4f + 0.1f... 0.2f
                var upgradeSpeed = Products.GetInnovationChance(product, gameContext) / 100f + UnityEngine.Random.Range(0.1f, 0.2f);

                var value = product.features.Upgrades[featureName];
                product.features.Upgrades[featureName] = Mathf.Clamp(value + upgradeSpeed, 0, 10f);
            } else
            {
                product.features.Upgrades[featureName] = UnityEngine.Random.Range(2, 5f);
            }

            var cooldownName = $"company-{product.company.Id}-upgradeFeature-{featureName}";
            Cooldowns.AddSimpleCooldown(gameContext, cooldownName, Products.GetBaseIterationTime(gameContext, product));
        }

        public static float GetFeatureRating(GameEntity product, string featureName)
        {
            if (IsUpgradedFeature(product, featureName))
                return product.features.Upgrades[featureName];

            return 0;
        }

        // in percents 0...15%
        public static float GetFeatureMaxBenefit(GameEntity product, NewProductFeature feature)
        {
            return GetFeatureActualBenefit(10, feature);
        }

        // feature benefits
        public static float GetFeatureActualBenefit(GameEntity product, string featureName)
        {
            var feature = GetAvailableFeaturesForProduct(product).First(f => f.Name == featureName);

            return GetFeatureActualBenefit(product, feature);
        }
        public static float GetFeatureActualBenefit(GameEntity product, NewProductFeature feature)
        {
            return GetFeatureActualBenefit(GetFeatureRating(product, feature.Name), feature);
        }
        public static float GetFeatureActualBenefit(float rating, NewProductFeature feature)
        {
            return rating * feature.FeatureBonus.Max / 10f;
        }


        public static bool IsUpgradedFeature(GameEntity product, string featureName)
        {
            return product.features.Upgrades.ContainsKey(featureName);
        }



        public static int GetTotalDevelopmentEffeciency(GameContext gameContext, GameEntity product)
        {
            var teamSizeModifier = Products.GetTeamEffeciency(gameContext, product);

            // team lead
            // 0...50
            var managerBonus = Teams.GetTeamLeadDevelopmentTimeDiscount(gameContext, product);

            var speed = teamSizeModifier * (100 + managerBonus) / 100;

            return speed;
        }


        public static int GetTeamEffeciency(GameContext gameContext, GameEntity product)
        {
            return (int) (100 * GetTeamSizeMultiplier(gameContext, product));
        }

        public static float GetTeamSizeMultiplier(GameContext gameContext, GameEntity company)
        {
            // +1 - CEO
            var have     = Teams.GetTeamSize(company) + 1f;
            var required = Products.GetNecessaryAmountOfWorkers(company, gameContext) + 1f;

            if (have >= required)
                have = required;

            return have / required;
        }

        public static int GetIterationTimeCost(GameEntity product, GameContext gameContext)
        {
            var baseCost = Products.GetBaseIterationTime(gameContext, product);

            bool willInnovate = Products.IsWillInnovate(product, gameContext);

            var innovationPenalty = willInnovate ? 250 : 100;

            var isReleasedPenalty = product.isRelease ? 2 : 1;

            return baseCost * innovationPenalty * isReleasedPenalty;
        }

        public static int GetTimeToMarketFromScratch(GameEntity niche)
        {
            var demand = GetMarketDemand(niche);
            var iterationTime = GetBaseIterationTime(niche);

            return demand * iterationTime / 2 / 30;
        }
    }
}
