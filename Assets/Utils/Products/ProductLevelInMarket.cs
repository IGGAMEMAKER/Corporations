using Assets.Classes;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static int GetMarketDemand(GameEntity product, GameEntity niche)
        {
            return niche.segment.Segments[UserType.Core];
        }

        public static int GetMarketDemand(GameEntity product, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            return GetMarketDemand(product, niche);
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
            var marketDemand = GetMarketDemand(product, niche);

            return current >= marketDemand;
        }

        public static bool IsOutOfMarket(GameEntity product, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            var current = GetProductLevel(product);
            var marketDemand = GetMarketDemand(product, niche);

            return current < marketDemand;
        }
    }
}
