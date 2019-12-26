using System.Linq;

namespace Assets.Core
{
    public static partial class MarketingUtils
    {
        public static float GetSumOfBrandPowers(NicheType nicheType, GameContext gameContext) => GetSumOfBrandPowers(Markets.GetNiche(gameContext, nicheType), gameContext);
        public static float GetSumOfBrandPowers(GameEntity niche, GameContext gameContext)
        {
            var products = Markets.GetProductsOnMarket(niche, gameContext);

            var sumOfBrandPowers = products.Sum(p => p.branding.BrandPower);

            return sumOfBrandPowers;
        }
        public static float GetBrandBasedMarketShare(GameEntity e, GameContext gameContext)
        {
            var niche = Markets.GetNiche(gameContext, e);

            var sumOfBrandPowers = GetSumOfBrandPowers(niche, gameContext);

            // +1 : avoid division by zero
            return e.branding.BrandPower / (sumOfBrandPowers + 1);
        }

        public static int GetBrandBasedAudienceGrowth(GameEntity e, GameContext gameContext)
        {
            var brandBasedMarketShare = GetBrandBasedMarketShare(e, gameContext);

            var flow = GetClientFlow(gameContext, e.product.Niche);

            return (int)(brandBasedMarketShare * flow);
        }

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
