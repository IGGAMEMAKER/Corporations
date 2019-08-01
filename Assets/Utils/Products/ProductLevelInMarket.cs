using Assets.Classes;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static int GetMarketDemand(GameEntity product, GameContext gameContext, UserType userType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            return niche.segment.Segments[userType];
        }

        public static bool IsInMarket(GameEntity product, GameContext gameContext)
        {
            return !IsWillInnovate(product, gameContext);
        }

        public static bool IsWillInnovate(GameEntity product, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            return IsWillInnovate(product, niche);
        }

        public static bool IsWillInnovate(GameEntity product, GameEntity niche)
        {
            var current = GetProductLevel(product);
            var marketDemand = niche.segment.Segments[UserType.Core];

            return current >= marketDemand;
        }
    }
}
