using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Products
    {
        public static SupportFeature[] GetAvailableSupportFeaturesForProduct(GameEntity product)
        {
            var usersPerServer = 50000;

            var million = 1000000;
            var billion = 1000000000;

            return new SupportFeature[]
            {
                new SupportFeature { Name = "1 server", SupportBonus = new SupportBonusHighload(usersPerServer) },
                new SupportFeature { Name = "5 servers", SupportBonus = new SupportBonusHighload(usersPerServer * 5) },
                new SupportFeature { Name = "Bug cluster", SupportBonus = new SupportBonusHighload(usersPerServer * 20) },
                new SupportFeature { Name = "Data center", SupportBonus = new SupportBonusHighload(usersPerServer * 100) },

                new SupportFeature { Name = "Small tech support", SupportBonus = new SupportBonusMarketingSupport(5000) },
                new SupportFeature { Name = "Average tech support", SupportBonus = new SupportBonusMarketingSupport(50000) },
                new SupportFeature { Name = "Big tech support", SupportBonus = new SupportBonusMarketingSupport(million) },
                new SupportFeature { Name = "Enormous tech support", SupportBonus = new SupportBonusMarketingSupport(billion) },
            };
        }

        // set of features
        public static SupportFeature[] GetHighloadFeatures(GameEntity product) => GetAvailableSupportFeaturesForProduct(product).Where(f => f.SupportBonus is SupportBonusHighload).ToArray();

        public static SupportFeature[] GetMarketingSupportFeatures(GameEntity product) => GetAvailableSupportFeaturesForProduct(product).Where(f => f.SupportBonus is SupportBonusMarketingSupport).ToArray();

        // benefits
        public static float GetMarketingSupportBenefit(GameEntity product)
        {
            return GetSummarySupportFeatureBenefit(product, GetMarketingSupportFeatures(product));
        }

        public static float GetHighloadFeaturesBenefit(GameEntity product)
        {
            return GetSummarySupportFeatureBenefit(product, GetHighloadFeatures(product));
        }

        public static bool IsNeedsMoreMarketingSupport(GameEntity product)
        {
            var capacity = GetMarketingSupportBenefit(product);
            var load = Marketing.GetClients(product); // add DDoS multiplier ??

            return load >= capacity;
        }

        public static bool IsNeedsMoreServers(GameEntity product)
        {
            var capacity = GetHighloadFeaturesBenefit(product);
            var load = Marketing.GetClients(product); // add DDoS multiplier

            return load >= capacity;
        }

        // summary feature benefit
        static float GetSummarySupportFeatureBenefit(GameEntity product, SupportFeature[] features)
        {
            var improvements = 0L;
            foreach (var f in features)
            {
                if (product.supportUpgrades.Upgrades.ContainsKey(f.Name))
                    improvements += product.supportUpgrades.Upgrades[f.Name] * f.SupportBonus.Max;
            }

            return improvements;
        }
    }
}
