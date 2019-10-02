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
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            var segments = niche.segment.Segments;

            var demand = GetMarketDemand(product, niche);
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

        public static bool IsTeamWorkingOnProduct(GameEntity product)
        {
            return TeamUtils.IsUpgradePicked(product, TeamUpgrade.DevelopmentPrototype) ||
            TeamUtils.IsUpgradePicked(product, TeamUpgrade.DevelopmentPolishedApp) ||
            TeamUtils.IsUpgradePicked(product, TeamUpgrade.DevelopmentCrossplatform);
        }

        // TODO DUPLICATE!! UpdateSegment Doesnot Use these functions
        public static void UpdgradeProduct(GameEntity product, GameContext gameContext)
        {
            if (CooldownUtils.HasConceptUpgradeCooldown(gameContext, product))
                return;

            if (!IsTeamWorkingOnProduct(product))
                return;

            var upgrade = 1;

            if (IsWillInnovate(product, gameContext))
            {
                // try to make the revolution
                var val = Random.Range(0, 100);

                var chance = ProductUtils.GetInnovationChance(product, gameContext);

                if (val > chance)
                    upgrade = 0;
            }

            var p = product.product;
            product.ReplaceProduct(p.Niche, GetProductLevel(product) + upgrade);

            UpdateNicheSegmentInfo(product, gameContext);

            var duration = GetProductUpgradeFinalIterationTime(gameContext, product);

            CooldownUtils.AddConceptUpgradeCooldown(gameContext, product, duration);
        }
    }
}
