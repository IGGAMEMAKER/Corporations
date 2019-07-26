using Assets.Classes;

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

        public static int GetMarketDemand(GameEntity product, GameContext gameContext, UserType userType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);
            
            return niche.segment.Segments[userType];
        }

        public static bool IsInMarket(GameEntity product, GameContext gameContext)
        {
            return !IsWillInnovate(product, gameContext, UserType.Core);
        }

        public static bool IsWillInnovate(GameEntity product, GameContext gameContext, UserType userType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            return IsWillInnovate(product, niche, userType);
        }

        public static bool IsWillInnovate(GameEntity product, GameEntity niche, UserType userType)
        {
            var current = product.product.Concept;
            var marketDemand = niche.segment.Segments[userType];

            return current >= marketDemand;
        }

        internal static int GetProductLevel(GameEntity c)
        {
            return c.product.Concept;
        }

        public static void UpdateNicheSegmentInfo(GameEntity product, GameContext gameContext, UserType userType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            UpdateNicheSegmentInfo(product, niche, userType);
        }

        public static void UpdateNicheSegmentInfo(GameEntity product, GameEntity niche, UserType userType)
        {
            var segments = niche.segment.Segments;

            if (IsWillInnovate(product, niche, userType))
            {
                segments[userType] = product.product.Concept + 1;

                niche.ReplaceSegment(segments);
            }
        }


        // TODO DUPLICATE!! UpdateSegment Doesnot Use these functions
        public static bool HasSegmentCooldown(GameEntity product, UserType userType)
        {
            var cooldown = new CooldownImproveSegment(userType);

            return CooldownUtils.HasCooldown(product, cooldown);
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

            if (CooldownUtils.HasCooldown(product, cooldown))
                return;

            var costs = GetSegmentUpgradeCost(product, gameContext, userType);

            if (!CompanyUtils.IsEnoughResources(product, costs))
                return;

            UpdateNicheSegmentInfo(product, gameContext, userType);

            var p = product.product;

            product.ReplaceProduct(p.Id, p.Niche, p.Concept + 1);


            var duration = GetSegmentImprovementDuration(gameContext, product);
            CooldownUtils.AddCooldownAndSpendResources(gameContext, product, cooldown, duration, costs);
        }

        public static int GetSegmentImprovementDuration(GameContext gameContext, GameEntity company)
        {
            var speed = TeamUtils.GetPerformance(gameContext, company);

            var innovation = IsWillInnovate(company, gameContext, UserType.Core) ? 4 : 1;

            var random = UnityEngine.Random.Range(1, 1.3f);

            return (int)(Constants.COOLDOWN_CONCEPT * random * innovation * 100 / speed);
        }

        public static int GetTotalImprovements(GameEntity product)
        {
            return product.product.Concept;
        }
    }
}
