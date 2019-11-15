using UnityEngine;

namespace Assets.Utils
{
    partial class EconomyUtils
    {
        // financing multipliers
        public static long GetMarketingFinancingCostMultiplier(GameEntity e) => GetMarketingFinancingCostMultiplier(e.financing.Financing[Financing.Marketing]);
        public static long GetMarketingFinancingCostMultiplier(int financing)
        {
            var marketing = MarketingUtils.GetAudienceReachModifierBasedOnMarketingFinancing(financing);

            return (long)Mathf.Pow(marketing, 1.8f);
        }

        public static long GetStageFinancingMultiplier(GameEntity e) => GetStageFinancingMultiplier(e.financing.Financing[Financing.Development]);
        public static long GetStageFinancingMultiplier(int financing)
        {
            switch (financing)
            {
                case 0: return 1;
                case 1: return 5;
                case 2: return 20;
                default: return -1000;
            }
        }

        public static float GetTeamFinancingMultiplier(GameEntity e) => GetTeamFinancingMultiplier(e.financing.Financing[Financing.Team]);
        public static float GetTeamFinancingMultiplier(int financing)
        {
            var acceleration = 1 + financing * Constants.FINANCING_ITERATION_SPEED_PER_LEVEL / 100f;

            return Mathf.Pow(acceleration, 10);
        }
    }
}
