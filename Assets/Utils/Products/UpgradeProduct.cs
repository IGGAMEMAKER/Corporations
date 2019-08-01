using Assets.Classes;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static void RemoveTechLeaders (GameEntity product, GameContext gameContext)
        {
            var players = NicheUtils.GetPlayersOnMarket(gameContext, product).ToArray();

            foreach (var p in players)
                p.isTechnologyLeader = false;
        }

        public static void UpdateNicheSegmentInfo(GameEntity product, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            var segments = niche.segment.Segments;

            if (IsWillInnovate(product, niche))
            {
                segments[UserType.Core] = GetProductLevel(product) + 1;

                MarketingUtils.AddBrandPower(product, Random.Range(3, 10));

                niche.ReplaceSegment(segments);

                RemoveTechLeaders(product, gameContext);
                product.isTechnologyLeader = true;
            } else if (IsInMarket(product, gameContext))
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

        // TODO DUPLICATE!! UpdateSegment Doesnot Use these functions
        public static void UpdateSegment(GameEntity product, GameContext gameContext)
        {
            var userType = UserType.Core;

            var cooldown = new CooldownImproveSegment(userType);

            if (CooldownUtils.HasCooldown(product, cooldown))
                return;

            var costs = GetProductUpgradeCost(product, gameContext);

            if (!CompanyUtils.IsEnoughResources(product, costs))
                return;

            var p = product.product;

            product.ReplaceProduct(p.Id, p.Niche, GetProductLevel(product) + 1);

            UpdateNicheSegmentInfo(product, gameContext);

            var duration = GetProductUpgradeIterationTime(gameContext, product);
            CooldownUtils.AddCooldownAndSpendResources(gameContext, product, cooldown, duration, costs);
        }

        public static bool HasEnoughResourcesForSegmentUpgrade(GameEntity product, GameContext gameContext)
        {
            var costs = GetProductUpgradeCost(product, gameContext);

            return CompanyUtils.IsEnoughResources(product, costs);
        }
    }
}
