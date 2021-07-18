using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        public static int GetBaseIterationTime(GameEntity company) => (int) GetBaseIterationTime(company, true).Sum(); 
        public static Bonus<float> GetBaseIterationTime(GameEntity company, bool isBonus)
        {
            var bonus = new Bonus<float>("Iteration");

            var features = company.productUpgrades.upgrades.Count;
            var appSize = 1f + features * 0.1f;

            var eff = Mathf.Clamp(Teams.GetDevelopmentEfficiency(company), 50, 150);

            bonus.Append("Base", 7);
            bonus.MultiplyAndHideIfOne("Application size", appSize);
            bonus.Multiply("Development Efficiency", 100f / eff);

            return bonus;
        }

        public static int GetIterationTime(GameEntity company)
        {
            return GetBaseIterationTime(company);

            ////var teamEffeciency = Teams.GetTeamAverageEffeciency(company);
            //var viableTeams = company.team.Teams
            //    // development only
            //    .Where(t => Teams.IsUniversalTeam(t.TeamType) || t.TeamType == TeamType.DevelopmentTeam)
            //    .Select(t => Teams.GetTeamEffeciency(company, t));


            //var teamEffeciency = viableTeams.Count() > 0 ? (int)viableTeams.Average() : 1;

            //return baseValue * 100 / teamEffeciency;
        }

        public static GameEntity GetWorkerInRole(TeamInfo team, WorkerRole workerRole, GameContext gameContext)
        {
            var productManagers = team.Managers.Select(humanId => Humans.Get(gameContext, humanId)).Where(worker => worker.worker.WorkerRole == workerRole);

            var managers = productManagers.ToList();
            if (managers.Any())
            {
                return managers.First();
            }

            return null;
        }

        public static float GetFeatureRatingGain(GameEntity product)
        {
            return product.teamEfficiency.Efficiency.FeatureGain;
        }

        public static float GetFeatureRatingCap(GameEntity product)
        {
            return product.teamEfficiency.Efficiency.FeatureCap;
        }

        public static void RemoveFeature(GameEntity product, string featureName, GameContext gameContext)
        {
            if (product.features.Upgrades.ContainsKey(featureName))
                product.features.Upgrades.Remove(featureName);
        }

        // TODO REMOVE? USED ONCE IN OLD TEAMTASK SYSTEM
        public static void IncreaseFeatureLevel(GameEntity product, string featureName)
        {
            var gain = GetFeatureRatingGain(product);
            var cap  = GetFeatureRatingCap(product);

            if (IsUpgradedFeature(product, featureName))
            {
                var value = product.features.Upgrades[featureName];

                // if new manager is worse than previous, this will not make feature worse!
                if (value > cap)
                    cap = value;

                product.features.Upgrades[featureName] = Mathf.Clamp(value + gain, 0, cap);
            } else
            {
                product.features.Upgrades[featureName] = gain; // UnityEngine.Random.Range(1, 3f);
            }
        }

        public static void AddFeatureCooldown(GameEntity product, TeamTask task, int currentDate)
        {
            task.EndDate = currentDate + GetIterationTime(product);
        }

        public static bool IsCanUpgradeFeatures(GameEntity product)
        {
            var cost = GetFeatureUpgradeCost();

            return Companies.IsEnoughResources(product, cost);
        }

        public static void TryToUpgradeFeature(GameEntity product, NewProductFeature feature, GameContext gameContext) => TryToUpgradeFeature(product, feature.Name, gameContext);
        public static void TryToUpgradeFeature(GameEntity product, string featureName, GameContext gameContext)
        {
            var rating = GetFeatureRating(product, featureName);

            if (IsCanUpgradeFeatures(product))
                ForceUpgradeFeature(product, featureName, rating + 1, gameContext);
        }

        public static TeamResource GetFeatureUpgradeCost() => new TeamResource(C.ITERATION_PROGRESS, 0, 0, 0, 0);

        public static void ForceUpgradeFeature(GameEntity product, string featureName, float value, GameContext gameContext)
        {
            var cost = GetFeatureUpgradeCost();

            product.features.Upgrades[featureName] = value;

            Companies.SpendResources(product, cost, "Feature");

            UpdateMarketRequirementsAndNotifyAllProductsAboutChanges(product, featureName, value, gameContext);
        }

        // OnFeatureUpgrade
        public static void UpdateMarketRequirementsAndNotifyAllProductsAboutChanges(GameEntity product, string featureName, float value, GameContext gameContext)
        {
            var niche = Markets.Get(gameContext, product);
            var index = product.features.Upgrades.Keys.ToList().IndexOf(featureName);

            if (!niche.hasMarketRequirements)
            {
                // TODO copied from CreateNicheMockup 
                niche.AddMarketRequirements(Products.GetAllFeaturesForProduct().Select(f => 0f).ToList());
            }

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

        public static float GetFeatureRating(GameEntity product, string featureName)
        {
            if (IsUpgradedFeature(product, featureName))
                return product.features.Upgrades[featureName];

            return 0;
        }

        public static bool IsLeadingInFeature(GameEntity Flagship, NewProductFeature Feature, GameContext Q)
        {
            var competitors = Companies.GetDirectCompetitors(Flagship, Q, true);

            return IsLeadingInFeature(Flagship, Feature, Q, competitors);
        }

        public static bool IsLeadingInFeature(GameEntity Flagship, NewProductFeature Feature, GameContext Q, IEnumerable<GameEntity> competitors)
        {
            var maxLVL = competitors.Max(c => GetFeatureRating(c, Feature.Name));

            return GetFeatureRating(Flagship, Feature.Name) == maxLVL;
        }

        // in percents 0...15%
        public static float GetFeatureMaxBenefit(GameEntity product, NewProductFeature feature)
        {
            return GetFeatureActualBenefit(10, feature);
        }

        // feature benefits
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



        public static int GetTimeToMarketFromScratch(GameEntity niche)
        {
            var demand = GetMarketDemand(niche);
            var iterationTime = 12; // GetBaseIterationTime(niche);

            return demand * iterationTime / 2 / 30;
        }
    }
}
