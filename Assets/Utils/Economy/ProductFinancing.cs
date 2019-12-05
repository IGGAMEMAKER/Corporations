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
    }
}
