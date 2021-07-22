using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Products
    {
        public static bool IsUpgradedFeature(GameEntity product, string featureName)
        {
            return product.features.Upgrades.ContainsKey(featureName);
        }

        public static int GetUpgradePoints(GameEntity product)
        {
            return product.companyResource.Resources.programmingPoints / C.ITERATION_PROGRESS;
        }

        public static int GetIterationProgress(GameEntity product)
        {
            var points = product.companyResource.Resources.programmingPoints;
            var iteration = points % C.ITERATION_PROGRESS;

            return iteration * 100 / C.ITERATION_PROGRESS;
        }

        public static int GetIterationMonthlyGain(GameEntity company)
        {
            var upgrades = company.team.Teams.Select(Economy.TeamFeatureGain).Sum() + 1;

            return upgrades * 4; // 4 cause month is 4 periods
        }





        public static float GetFeatureRating(GameEntity product, string featureName)
        {
            if (IsUpgradedFeature(product, featureName))
                return product.features.Upgrades[featureName];

            return 0;
        }

        public static bool IsLeadingInFeature(GameEntity Flagship, NewProductFeature Feature, GameContext Q)
        {
            //var competitors = Companies.GetDirectCompetitors(Flagship, Q, true);

            //return IsLeadingInFeature(Flagship, Feature, Q, competitors);
            var index = GetAllFeaturesForProduct().ToList().FindIndex(f => f.Name == Feature.Name);

            return Flagship.marketRequirements.Features[index] <= GetFeatureRating(Flagship, Feature.Name);
        }

        /*public static bool IsLeadingInFeature(GameEntity Flagship, NewProductFeature Feature, GameContext Q, IEnumerable<GameEntity> competitors)
        {
            //var maxLVL = competitors.Max(c => GetFeatureRating(c, Feature.Name));

            //return GetFeatureRating(Flagship, Feature.Name) == maxLVL;
        }*/

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



        public static int GetTimeToMarketFromScratch(GameEntity niche)
        {
            var demand = GetMarketDemand(niche);
            var iterationTime = 12; // GetBaseIterationTime(niche);

            return demand * iterationTime / 2 / 30;
        }

        // TODO REMOVE??
        public static float GetFeatureRatingGain(GameEntity product)
        {
            return product.teamEfficiency.Efficiency.FeatureGain;
        }

        public static float GetFeatureRatingCap(GameEntity product)
        {
            return product.teamEfficiency.Efficiency.FeatureCap;
        }
    }
}
