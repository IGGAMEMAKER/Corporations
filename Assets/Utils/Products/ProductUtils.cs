using Assets.Classes;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static void SetPrice(GameEntity e, Pricing pricing)
        {
            e.ReplaceFinance(pricing, e.finance.marketingFinancing, e.finance.salaries, e.finance.basePrice);
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
            var current = GetProductLevel(product);
            var marketDemand = niche.segment.Segments[userType];

            return current >= marketDemand;
        }


        internal static int GetProductLevel(GameEntity c)
        {
            return c.product.Concept;
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
    }
}
