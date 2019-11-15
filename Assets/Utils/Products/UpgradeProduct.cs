using Assets.Classes;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static void RemoveTechLeaders (GameEntity product, GameContext gameContext)
        {
            var players = NicheUtils.GetProductsOnMarket(gameContext, product).ToArray();

            foreach (var p in players)
                p.isTechnologyLeader = false;
        }

        public static void UpdateNicheSegmentInfo(GameEntity product, GameContext gameContext)
        {
            var niche = NicheUtils.GetNiche(gameContext, product.product.Niche);

            var segments = niche.segment.Segments;

            var demand = GetMarketDemand(niche);
            var newLevel = GetProductLevel(product);

            if (newLevel > demand)
            {
                segments[UserType.Core] = newLevel;

                MarketingUtils.AddBrandPower(product, Random.Range(3, 10));

                niche.ReplaceSegment(segments);

                RemoveTechLeaders(product, gameContext);
                product.isTechnologyLeader = true;
            } else if (newLevel == demand)
            {
                RemoveTechLeaders(product, gameContext);
            }
        }

        public static void UpgradeExpertise(GameEntity product, GameContext gameContext)
        {
            var nicheCosts = NicheUtils.GetNicheCosts(gameContext, product.product.Niche);
            var baseIdeaCost = nicheCosts.IdeaCost;

            var expertise = product.expertise.ExpertiseLevel;

            var required = new TeamResource(0, 0, 0, baseIdeaCost * expertise, 0);

            if (CompanyUtils.IsEnoughResources(product, required))
            {
                CompanyUtils.SpendResources(product, required);
                product.ReplaceExpertise(expertise + 1);
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

                if (Random.Range(0, 100) > chance)
                    upgrade = 0;
            }

            var p = product.product;
            product.ReplaceProduct(p.Niche, GetProductLevel(product) + upgrade);

            UpdateNicheSegmentInfo(product, gameContext);

            if (!IgnoreCooldowns)
            {
                var duration = GetProductUpgradeFinalIterationTime(gameContext, product);

                CooldownUtils.AddConceptUpgradeCooldown(gameContext, product, duration);
            }
        }
    }
}
