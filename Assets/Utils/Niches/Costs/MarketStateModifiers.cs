namespace Assets.Utils
{
    public static partial class Markets
    {
        public static float GetMarketStatePriceModifier(NicheState phase)
        {
            switch (phase)
            {
                case NicheState.Innovation:    return 1;
                case NicheState.Trending:      return 0.8f;
                case NicheState.MassGrowth:    return 0.7f;
                case NicheState.MassUsage:     return 0.65f;
                case NicheState.Decay:         return 0.5f;

                default: return 0;
            }
        }

        public static float GetMarketStateAdCostModifier(NicheState phase)
        {
            switch (phase)
            {
                case NicheState.Innovation:    return 0.1f;
                case NicheState.Trending:      return 0.5f;
                case NicheState.MassGrowth:    return 1f;
                case NicheState.Decay:         return 2f;

                default: return 0;
            }
        }

        // TODO move this to niche utils!!!
        public static string GetMarketStateDescription(NicheState state)
        {
            switch (state)
            {
                case NicheState.Idle: return "???";
                case NicheState.Innovation: return "Innovation";
                case NicheState.Trending: return "Trending";
                case NicheState.MassGrowth: return "Mass growth";
                case NicheState.MassUsage: return "Mass use";
                case NicheState.Decay: return "Decay";
                case NicheState.Death: return "Death";

                default: return "???WTF " + state.ToString();
            }
        }

        internal static long GetMarketStageInnovationModifier(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            switch (phase)
            {
                case NicheState.Death:
                case NicheState.Decay:
                case NicheState.Idle:
                    return 0;

                case NicheState.Innovation:
                    return 25;

                case NicheState.Trending:
                    return 15;

                case NicheState.MassGrowth:
                    return 5;

                default: return 0;
            }
        }

        // Unnecessary
        public static int GetMarketRating(GameEntity niche)
        {
            var phase = GetMarketState(niche);
            switch (phase)
            {
                case NicheState.Idle: return 1;
                case NicheState.Innovation: return 3;
                case NicheState.Trending: return 4;
                case NicheState.MassGrowth: return 5;
                case NicheState.Decay: return 2;

                default:
                    return 0;
            }
        }

        public static int GetMarketDemandRisk(GameContext gameContext, NicheType nicheType)
        {
            var phase = GetMarketState(gameContext, nicheType);

            switch (phase)
            {
                case NicheState.Idle:
                    return Constants.RISKS_DEMAND_MAX;

                case NicheState.Innovation:
                    return Constants.RISKS_DEMAND_MAX / 2;

                case NicheState.Trending:
                    return Constants.RISKS_DEMAND_MAX / 5;

                case NicheState.MassGrowth:
                    return Constants.RISKS_DEMAND_MAX / 10;

                case NicheState.Decay:
                    return Constants.RISKS_DEMAND_MAX / 2;

                case NicheState.Death:
                default:
                    return 100;
            }
        }

        // niche lifecycle
        private static int GetNichePeriodDurationInMonths(NicheState phase)
        {
            switch (phase)
            {
                case NicheState.Innovation: return 8;
                case NicheState.Trending: return 24;
                case NicheState.MassGrowth: return 60;
                case NicheState.MassUsage: return 120;
                case NicheState.Decay: return 100;
                default: return 1;
            }
        }
        public static NicheState GetNextPhase(NicheState phase)
        {
            switch (phase)
            {
                case NicheState.Idle: return NicheState.Innovation;
                case NicheState.Innovation: return NicheState.Trending;
                case NicheState.Trending: return NicheState.MassGrowth; 
                case NicheState.MassGrowth: return NicheState.MassUsage;
                case NicheState.MassUsage: return NicheState.Decay;

                case NicheState.Decay:
                default:
                    return NicheState.Death;
            }
        }
        public static float GetMonthlyAudienceGrowthMultiplier(NicheState phase)
        {
            return 1.05f;
            switch (phase)
            {
                case NicheState.Idle:
                    return 1;

                case NicheState.Innovation:
                    return 1.01f;

                case NicheState.Trending:
                    return 1.02f;

                case NicheState.MassGrowth:
                    return 1.05f;
            }

        }

        public static int GetAudiencePercentageThatProductsWillGetDuringThisMarketState(NicheState phase)
        {
            switch (phase)
            {
                case NicheState.Innovation:
                    return 10;

                case NicheState.Trending:
                    return 25;

                case NicheState.MassGrowth:
                    return 55;

                case NicheState.Decay:
                    return 10;

                case NicheState.Death:
                case NicheState.Idle:
                default:
                    return 0;
            }
        }
    }
}
