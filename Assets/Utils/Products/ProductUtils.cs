using Assets.Classes;
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
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            bool isInnovation = IsWillInnovate(product, niche, userType);

            var innovationModifier = isInnovation ? 3 : 1;

            var costs = niche.nicheCosts;

            return new TeamResource(costs.TechCost * innovationModifier, 0, 0, costs.IdeaCost * innovationModifier, 0);
        }

        public static int GetSegmentMarketDemand(GameEntity product, GameContext gameContext, UserType userType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            return niche.segment.Segments[userType];
        }

        public static bool IsWillInnovate(GameEntity product, GameContext gameContext, UserType userType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            return IsWillInnovate(product, niche, userType);
        }

        public static bool IsWillInnovate(GameEntity product, GameEntity niche, UserType userType)
        {
            var current = product.product.Segments[userType];
            var marketDemand = niche.segment.Segments[userType];

            return current >= marketDemand;
        }

        public static void UpdateNicheSegmentInfo(GameEntity product, GameContext gameContext, UserType userType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            Debug.Log($"UpdateNicheSegmentInfo {product.company.Name} {userType}. {product.product.Segments[userType]}/{niche.segment.Segments[userType]} ");

            UpdateNicheSegmentInfo(product, niche, userType);
        }

        public static void UpdateNicheSegmentInfo(GameEntity product, GameEntity niche, UserType userType)
        {
            var segments = niche.segment.Segments;

            if (IsWillInnovate(product, niche, userType))
            {
                segments[userType] = product.product.Segments[userType] + 1;

                niche.ReplaceSegment(segments);
            }
        }


        // TODO DUPLICATE!! UpdateSegment Doesnot Use these functions
        public static bool HasSegmentCooldown(GameEntity product, UserType userType)
        {
            var cooldown = new CooldownImproveSegment(userType);

            return CompanyUtils.HasCooldown(product, cooldown);
        }

        public static bool HasEnoughResourcesForSegmentUpgrade(GameEntity product, GameContext gameContext, UserType userType)
        {
            var costs = GetSegmentUpgradeCost(product, gameContext, userType);

            return CompanyUtils.IsEnoughResources(product, costs);
        }

        // TODO DUPLICATE!! UpdateSegment Doesnot Use these functions
        public static void UpdateSegment(GameEntity product, GameContext gameContext, UserType userType1)
        {
            var userType = UserType.Core;

            var cooldown = new CooldownImproveSegment(userType);

            if (CompanyUtils.HasCooldown(product, cooldown))
                return;

            var costs = GetSegmentUpgradeCost(product, gameContext, userType);

            if (!CompanyUtils.IsEnoughResources(product, costs))
                return;

            UpdateNicheSegmentInfo(product, gameContext, userType);

            var p = product.product;

            var dict = p.Segments;

            dict[userType]++;

            product.ReplaceProduct(p.Id, p.Niche, dict);


            CompanyUtils.SpendResources(product, costs);
            CompanyUtils.AddCooldown(gameContext, product, cooldown, 15);
        }

        public static int GetTotalImprovements(GameEntity product)
        {
            var segments = product.product.Segments;

            return segments[UserType.Core] + segments[UserType.Mass] + segments[UserType.Regular];
        }
    }
}
