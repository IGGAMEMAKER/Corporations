using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Products
    {
        public static SupportFeature[] GetAvailableSupportFeaturesForProduct(GameEntity product)
        {
            return new SupportFeature[]
            {
                new SupportFeature { Name = "1 server", SupportBonus = new SupportBonusHighload(1000) },
                new SupportFeature { Name = "5 servers", SupportBonus = new SupportBonusHighload(6000) },
                new SupportFeature { Name = "Data center", SupportBonus = new SupportBonusHighload(1000000) },

                new SupportFeature { Name = "Small tech support", SupportBonus = new SupportBonusMarketingSupport(5000) },
                new SupportFeature { Name = "Average tech support", SupportBonus = new SupportBonusMarketingSupport(50000) },
                new SupportFeature { Name = "Big tech support", SupportBonus = new SupportBonusMarketingSupport(500000) },
                new SupportFeature { Name = "Enormous tech support", SupportBonus = new SupportBonusMarketingSupport(100000000) },
            };
        }

        // set of features
        public static SupportFeature[] GetHighloadFeatures(GameEntity product)
        {
            return GetAvailableSupportFeaturesForProduct(product).Where(f => f.SupportBonus is SupportBonusHighload).ToArray();
        }

        public static SupportFeature[] GetMarketingSupportFeatures(GameEntity product)
        {
            return GetAvailableSupportFeaturesForProduct(product).Where(f => f.SupportBonus is SupportBonusMarketingSupport).ToArray();
        }

        // set ot feature benefits
        public static float GetMarketingSupportBenefit(GameEntity product)
        {
            return GetSummarySupportFeatureBenefit(product, GetMarketingSupportFeatures(product));
        }

        public static float GetHighloadFeaturesBenefit(GameEntity product)
        {
            return GetSummarySupportFeatureBenefit(product, GetHighloadFeatures(product));
        }

        // summary feature benefit
        static float GetSummarySupportFeatureBenefit(GameEntity product, SupportFeature[] features)
        {
            var improvements = 0L;
            foreach (var f in features)
            {
                improvements += product.supportUpgrades.Upgrades[f.Name] * f.SupportBonus.Max;
            }

            return improvements;
        }


        //public static float GetSupportFeatureActualBenefit(GameEntity product, string featureName)
        //{
        //    var feature = GetAvailableSupportFeaturesForProduct(product).First(f => f.Name == featureName);

        //    return GetSupportFeatureActualBenefit(product, feature);
        //}
        //public static float GetSupportFeatureActualBenefit(GameEntity product, SupportFeature feature)
        //{
        //    return GetSupportFeatureActualBenefit(GetFeatureRating(product, feature.Name), feature);
        //}
        //public static float GetSupportFeatureActualBenefit(float rating, SupportFeature feature)
        //{
        //    return rating * feature.SupportBonus.Max / 10f;
        //}
    }
}
