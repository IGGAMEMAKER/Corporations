using Assets.Classes;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class Products
    {
        public static void RemoveTechLeaders (GameEntity product, GameContext gameContext)
        {
            var players = Markets.GetProductsOnMarket(gameContext, product).ToArray();

            foreach (var p in players)
                p.isTechnologyLeader = false;
        }

        public static void UpdateNicheSegmentInfo(GameEntity product, GameContext gameContext)
        {
            var niche = Markets.GetNiche(gameContext, product.product.Niche);

            var segments = niche.segment.Level;

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

        public static void UpgradeProductImprovement(ProductImprovement improvement, GameEntity product)
        {
            var level = GetProductLevel(product);

            if (product.productImprovements.Count < level)
            {
                product.productImprovements.Improvements[improvement]++;
                product.productImprovements.Count++;
            }
        }


        public static void UpdgradeProduct(GameEntity product, GameContext gameContext, bool IgnoreCooldowns = false)
        {
            if (CooldownUtils.HasConceptUpgradeCooldown(gameContext, product) && !IgnoreCooldowns)
                return;

            var upgrade = 1;

            if (IsWillInnovate(product, gameContext))
            {
                var chance = GetInnovationChance(product, gameContext);

                if (Random.Range(0, 100) < chance)
                    upgrade = 0;
            }

            product.ReplaceProduct(product.product.Niche, GetProductLevel(product) + upgrade);

            UpdateNicheSegmentInfo(product, gameContext);

            if (IgnoreCooldowns)
                return;

            var duration = GetProductUpgradeIterationTime(gameContext, product);

            CooldownUtils.AddConceptUpgradeCooldown(gameContext, product, duration);
        }
    }
}
