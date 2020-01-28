using UnityEngine;

namespace Assets.Core
{
    partial class Economy
    {
        // financing multipliers
        public static long GetMarketingFinancingCostMultiplier(GameEntity product, GameContext gameContext) => GetMarketingFinancingCostMultiplier(product, gameContext, GetMarketingFinancing(product));
        public static long GetMarketingFinancingCostMultiplier(GameEntity product, GameContext gameContext, int financing) {
            var clientCost = Markets.GetClientAcquisitionCost(product.product.Niche, gameContext);
            var flow = Marketing.GetClientFlow(gameContext, product.product.Niche);

            // not released
            // needs to depend on own size
            if (financing == 0)
            {
                return (long)(clientCost * flow / 10);
            }

            // released
            if (financing == 1)
            {

                return (long)(clientCost * flow);
            }

            // capturing market
            var audience = Markets.GetAudienceSize(gameContext, product.product.Niche);

            return (long)(clientCost * audience);

            //var marketing = MarketingUtils.GetAudienceReachModifierBasedOnMarketingFinancing(financing);

            //return Mathf.Pow(marketing, 1.78f);
        }

        public static int GetNextMarketingFinancing(GameEntity e)
        {
            return Mathf.Clamp(GetMarketingFinancing(e) + 1, 0, Products.GetMaxFinancing);
        }

        public static int GetCheaperFinancing(GameEntity e)
        {
            return Mathf.Clamp(GetMarketingFinancing(e) - 1, e.isRelease ? 1 : 0, Products.GetMaxFinancing);
        }

        public static int GetMarketingFinancing(GameEntity e)
        {
            return e.financing.Financing[Financing.Marketing];
        }

        public static long GetNextMarketingLevelCost(GameEntity e, GameContext gameContext)
        {
            var financing = GetMarketingFinancing(e);
            var nextFinancing = GetNextMarketingFinancing(e);

            var nextCost = GetMarketingFinancingCostMultiplier(e, gameContext, nextFinancing);

            return nextCost;
        }
    }
}
