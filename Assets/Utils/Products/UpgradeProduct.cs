using Assets.Classes;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class Products
    {
        public static void UpdgradeProduct(GameEntity product, GameContext gameContext, bool IgnoreCooldowns = false)
        {
            if (CooldownUtils.HasConceptUpgradeCooldown(gameContext, product) && !IgnoreCooldowns)
                return;

            TryToUpgradeProduct(product, gameContext);
            UpdateNicheSegmentInfo(product, gameContext);

            if (IgnoreCooldowns)
                return;

            var duration = GetProductUpgradeIterationTime(gameContext, product) * Random.Range(10, 11) / 10;

            CooldownUtils.AddConceptUpgradeCooldown(gameContext, product, duration);
        }

        private static void TryToUpgradeProduct(GameEntity product, GameContext gameContext)
        {
            // upgrade by default
            var upgrade = 1;

            if (IsWillInnovate(product, gameContext))
            {
                var chance = GetInnovationChance(product, gameContext);

                // or not, if unsuccessful
                if (Random.Range(0, 100) > chance)
                    upgrade = 0;
            }

            product.ReplaceProduct(product.product.Niche, GetProductLevel(product) + upgrade);
        }

        private static void UpdateNicheSegmentInfo(GameEntity product, GameContext gameContext)
        {
            var niche = Markets.GetNiche(gameContext, product.product.Niche);

            var demand = GetMarketDemand(niche);
            var newLevel = GetProductLevel(product);

            if (newLevel > demand)
            {
                MarketingUtils.AddBrandPower(product, Random.Range(10, 20));

                niche.ReplaceSegment(newLevel);

                RemoveTechLeaders(product, gameContext);
                product.isTechnologyLeader = true;
            } else if (newLevel == demand)
            {
                // if you are techonology leader and you fail to innovate, you will not lose tech leadership
                if (product.isTechnologyLeader)
                    return;

                RemoveTechLeaders(product, gameContext);
            }
        }

        private static void RemoveTechLeaders(GameEntity product, GameContext gameContext)
        {
            var players = Markets.GetProductsOnMarket(gameContext, product).ToArray();

            foreach (var p in players)
                p.isTechnologyLeader = false;
        }


        // TODO move to separate file/delete
        public static void UpgradeProductImprovement(ProductImprovement improvement, GameEntity product)
        {
            var level = GetProductLevel(product);

            if (product.productImprovements.Count < level)
            {
                product.productImprovements.Improvements[improvement]++;
                product.productImprovements.Count++;
            }
        }
    }
}
