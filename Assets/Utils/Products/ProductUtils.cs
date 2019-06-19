using Assets.Classes;
using Entitas;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static void SetPrice(GameEntity e, Pricing pricing)
        {
            e.ReplaceFinance(pricing, e.finance.marketingFinancing, e.finance.salaries, e.finance.basePrice);
        }

        public static TeamResource GetSegmentUpgradeCost(GameEntity product, GameContext gameContext, UserType userType)
        {
            var costs = NicheUtils.GetNicheEntity(gameContext, product.product.Niche).nicheCosts;

            return new TeamResource(costs.TechCost / 3, 0, 0, costs.IdeaCost / 3, 0);
        }

        public static void UpdateSegment(GameEntity product, GameContext gameContext, UserType userType)
        {
            var cooldown = new CooldownImproveSegment(userType);

            if (CompanyUtils.HasCooldown(product, cooldown))
                return;

            var costs = GetSegmentUpgradeCost(product, gameContext, userType);

            if (!CompanyUtils.IsEnoughResources(product, costs))
                return;

            var p = product.product;

            var dict = p.Segments;

            dict[userType]++;

            product.ReplaceProduct(p.Id, p.Niche, dict);

            CompanyUtils.SpendResources(product, costs);
            CompanyUtils.AddCooldown(gameContext, product, cooldown, 65);
        }

        public static int GetTotalImprovements(GameEntity product)
        {
            var segments = product.product.Segments;

            return segments[UserType.Core] + segments[UserType.Mass] + segments[UserType.Regular];
        }
    }
}
