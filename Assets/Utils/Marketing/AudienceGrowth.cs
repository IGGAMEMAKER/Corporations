using System.Linq;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static float GetBrandBasedMarketShare(GameEntity e, GameContext gameContext)
        {
            var products = NicheUtils.GetProductsOnMarket(gameContext, e);

            var sumOfBrandPowers = products.Sum(p => p.branding.BrandPower) + 1;

            return e.branding.BrandPower / sumOfBrandPowers;
        }

        public static int GetBrandBasedAudienceGrowth(GameEntity e, GameContext gameContext)
        {
            var brandBasedMarketShare = GetBrandBasedMarketShare(e, gameContext);

            var flow = GetClientFlow(gameContext, e.product.Niche);

            return (int)(brandBasedMarketShare * flow);
        }

        // outdated
        public static long GetAudienceGrowth(GameEntity product, GameContext gameContext)
        {
            return GetBrandBasedAudienceGrowth(product, gameContext);
        }

        // based on financing
        public static float GetAudienceReachModifierBasedOnMarketingFinancing(int financing)
        {
            switch (financing)
            {
                case 0: return 1;
                case 1: return 2;
                case 2: return 5;
                case 3: return 10;
                default: return 10000;
            }
        }
    }
}
