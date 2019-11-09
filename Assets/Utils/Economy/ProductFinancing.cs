using UnityEngine;

namespace Assets.Utils
{
    partial class EconomyUtils
    {
        public static long GetMarketingFinancingMultiplier(GameEntity e) => GetMarketingFinancingMultiplier(e.financing.Financing[Financing.Marketing]);
        public static long GetStageFinancingMultiplier(GameEntity e) => GetStageFinancingMultiplier(e.financing.Financing[Financing.Development]);
        public static float GetTeamFinancingMultiplier(GameEntity e) => GetTeamFinancingMultiplier(e.financing.Financing[Financing.Team]);

        public static long GetProductMarketingCost(GameEntity e, GameContext gameContext)
        {
            var multiplier = GetMarketingFinancingMultiplier(e);

            var baseCost = NicheUtils.GetBaseMarketingCost(e.product.Niche, gameContext);

            return baseCost * multiplier;
        }
        public static long GetProductDevelopmentCost(GameEntity e, GameContext gameContext)
        {
            var stage = GetStageFinancingMultiplier(e);
            var team = GetTeamFinancingMultiplier(e);

            var baseCost = NicheUtils.GetBaseDevelopmentCost(e.product.Niche, gameContext);

            return (long)(baseCost * stage * team);
        }

        internal static long GetProductCompanyMaintenance(GameEntity e, GameContext gameContext)
        {
            var devFinancing = GetProductDevelopmentCost(e, gameContext);
            var marketingFinancing = GetProductMarketingCost(e, gameContext);

            return devFinancing + marketingFinancing;
        }




        public static long GetMarketingFinancingMultiplier (int financing)
        {
            switch (financing)
            {
                case 0: return 0;
                case 1: return 1;
                case 2: return 5;
                case 3: return 20;
                default: return -1000;
            }
        }
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
        public static float GetTeamFinancingMultiplier (int financing)
        {
            var acceleration = 1 + financing * Constants.FINANCING_ITERATION_SPEED_PER_LEVEL / 100f;

            return Mathf.Pow(acceleration, 10);
        }


        public static long GetTeamFinancingEffeciency (int financing)
        {
            switch (financing)
            {
                case 0: return 100; // 100
                case 1: return 120; // 120
                case 2: return 200; // 200
                case 3: return 300; // 300
                default: return -1000; // 
            }
        }




    }
}
