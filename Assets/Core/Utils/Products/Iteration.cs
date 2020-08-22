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

        public static GameEntity GetWorkerInRole(TeamInfo team, WorkerRole workerRole, GameContext gameContext)
        {
            var productManagers = team.Managers.Select(humanId => Humans.GetHuman(gameContext, humanId)).Where(worker => worker.worker.WorkerRole == workerRole);

            if (productManagers.Count() > 0)
            {
                return productManagers.First();
            }

            return null;
        }

        public static float GetFeatureUpgradeRatingGain(GameEntity product, TeamInfo team, GameContext gameContext)
        {
            var speed = 0.2f;

            bool isDevTeam = team.TeamType == TeamType.DevelopmentTeam;

            var productManager = GetWorkerInRole(team, WorkerRole.ProductManager, gameContext);

            if (productManager != null)
            {

                // 0.4f ... 1f
                var gain = Humans.GetRating(productManager) / 100f;
                speed += isDevTeam ? gain * 2 : gain;
            }

            if (isDevTeam)
            {
                speed += 0.3f;
            }

            return speed;
        }

        public static float GetFeatureRatingCap(GameEntity product, TeamInfo team, GameContext gameContext)
        {
            var productManager = GetWorkerInRole(team, WorkerRole.ProductManager, gameContext);

            var cap = 5f;

            if (productManager != null)
            {
                // ... 5f
                var addedCap = 5 * Humans.GetRating(productManager) / 100f;

                return cap + addedCap;
            }

            return cap;
        }

        public static void UpgradeFeature(GameEntity product, string featureName, GameContext gameContext, TeamInfo team)
        {
            if (IsUpgradedFeature(product, featureName))
            {
                var gain = GetFeatureUpgradeRatingGain(product, team, gameContext);

                var value = product.features.Upgrades[featureName];

                var cap = GetFeatureRatingCap(product, team, gameContext);

                // if new manager is worse than previous, this will not make feature worse!
                if (value > cap)
                    cap = value;

                product.features.Upgrades[featureName] = Mathf.Clamp(value + gain, 0, cap);
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

        public static int GetTimeToMarketFromScratch(GameEntity niche)
        {
            var demand = GetMarketDemand(niche);
            var iterationTime = GetBaseIterationTime(niche);

            return demand * iterationTime / 2 / 30;
        }
    }
}
