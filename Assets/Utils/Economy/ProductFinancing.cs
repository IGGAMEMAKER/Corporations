using UnityEngine;

namespace Assets.Utils
{
    partial class EconomyUtils
    {
        // financing multipliers
        public static float GetMarketingFinancingCostMultiplier(GameEntity e) => GetMarketingFinancingCostMultiplier(e.financing.Financing[Financing.Marketing]);
        public static float GetMarketingFinancingCostMultiplier(int financing)
        {
            var marketing = MarketingUtils.GetAudienceReachModifierBasedOnMarketingFinancing(financing);

            return Mathf.Pow(marketing, 1.28f);
        }

        public static int GetNextMarketingFinancing(GameEntity e)
        {
            return Mathf.Clamp(GetMarketingFinancing(e) + 1, 0, ProductUtils.GetMaxFinancing);
        }

        public static int GetCheaperFinancing(GameEntity e)
        {
            return Mathf.Clamp(GetMarketingFinancing(e) - 1, e.isRelease ? 1 : 0, ProductUtils.GetMaxFinancing);
        }

        public static int GetMarketingFinancing(GameEntity e)
        {
            return e.financing.Financing[Financing.Marketing];
        }

        public static long GetNextMarketingLevelCost(GameEntity e, GameContext gameContext)
        {
            var financing = GetMarketingFinancing(e);
            var nextFinancing = GetNextMarketingFinancing(e);

            var nextCost = GetProductMarketingCost(e, gameContext, nextFinancing);

            return nextCost;
        }
    }
}
