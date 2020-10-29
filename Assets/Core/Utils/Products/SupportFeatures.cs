using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Products
    {
        public static SupportFeature[] GetAvailableSupportFeaturesForProduct(GameEntity product)
        {
            var usersPerServer = 100_000;

            var million = 1_000_000;
            var billion = 1_000_000_000;

            return new SupportFeature[]
            {
                new SupportFeature { Name = "1 server", SupportBonus = new SupportBonusHighload(usersPerServer) },
                new SupportFeature { Name = "Cluster", SupportBonus = new SupportBonusHighload(usersPerServer * 5) },
                new SupportFeature { Name = "Big cluster", SupportBonus = new SupportBonusHighload(usersPerServer * 20) },
                new SupportFeature { Name = "Data center", SupportBonus = new SupportBonusHighload(usersPerServer * 1000) },

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
        public static long GetMarketingSupportBenefit(GameEntity product)
        {
            return GetSummarySupportFeatureBenefit(product, GetMarketingSupportFeatures(product));
        }

        public static long GetHighloadFeaturesBenefit(GameEntity product)
        {
            return GetSummarySupportFeatureBenefit(product, GetHighloadFeatures(product));
        }


        public static long GetClientLoad(GameEntity product)
        {
            long attack = 0;

            if (product.hasServerAttack)
            {
                attack = product.serverAttack.Load;
            }

            return Marketing.GetUsers(product) + attack; // add DDoS multiplier ??
        }

        public static long GetServerLoad(GameEntity product)
        {
            return GetClientLoad(product);
        }

        public static long GetSupportLoad(GameEntity product)
        {
            return GetClientLoad(product);
        }

        public static long GetServerCapacity(GameEntity product)
        {
            if (!product.isFlagship)
                return 1_000_000_000;

            return GetHighloadFeaturesBenefit(product);
        }

        public static long GetSupportCapacity(GameEntity product)
        {
            return GetMarketingSupportBenefit(product);
        }

        public static bool IsNeedsMoreMarketingSupport(GameEntity product)
        {
            var capacity = GetSupportCapacity(product);
            var load = GetClientLoad(product);

            return false;
            return load >= capacity + C.MINIMUM_SUPPORTED_CLIENTS;
        }

        public static bool IsNeedsMoreServers(GameEntity product)
        {
            // hack to avoid AI client loss
            if (!product.isFlagship)
                return false;

            var capacity = GetServerCapacity(product);
            var load = GetClientLoad(product); // add DDoS multiplier

            return load >= capacity;
        }

        // summary feature benefit
        static long GetSummarySupportFeatureBenefit(GameEntity product, SupportFeature[] features)
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
