namespace Assets.Utils
{
    public static partial class Markets
    {
        public static float GetMarketStatePriceModifier(MarketState phase)
        {
            switch (phase)
            {
                case MarketState.Innovation:    return 1;
                case MarketState.Trending:      return 0.8f;
                case MarketState.MassGrowth:    return 0.7f;
                case MarketState.MassUsage:     return 0.65f;
                case MarketState.Decay:         return 0.5f;

                default: return 0;
            }
        }

        public static float GetMarketStateAdCostModifier(MarketState phase)
        {
            switch (phase)
            {
                case MarketState.Innovation:    return 0.1f;
                case MarketState.Trending:      return 0.5f;
                case MarketState.MassGrowth:    return 1f;
                case MarketState.MassUsage:     return 1f;
                case MarketState.Decay:         return 2f;

                default: return 0;
            }
        }

        // TODO move this to niche utils!!!
        public static string GetMarketStateDescription(MarketState state)
        {
            switch (state)
            {
                case MarketState.Idle: return "???";
                case MarketState.Innovation: return "Innovation";
                case MarketState.Trending: return "Trending";
                case MarketState.MassGrowth: return "Mass growth";
                case MarketState.MassUsage: return "Mass use";
                case MarketState.Decay: return "Decay";
                case MarketState.Death: return "Death";

                default: return "???WTF " + state.ToString();
            }
        }

        internal static long GetMarketStageInnovationModifier(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            switch (phase)
            {
                case MarketState.Innovation:    return 30;
                case MarketState.Trending:      return 20;

                case MarketState.MassGrowth:
                case MarketState.MassUsage:
                    return 10;

                default: return 0;
            }
        }

        // Unnecessary
        public static int GetMarketRating(GameEntity niche)
        {
            var phase = GetMarketState(niche);
            switch (phase)
            {
                case MarketState.Idle:          return 1;
                case MarketState.Innovation:    return 3;
                case MarketState.Trending:      return 4;
                case MarketState.MassGrowth:    return 5;
                case MarketState.MassUsage:     return 5;
                case MarketState.Decay:         return 2;

                default:
                    return 0;
            }
        }

        public static int GetMarketDemandRisk(GameContext gameContext, NicheType nicheType)
        {
            var phase = GetMarketState(gameContext, nicheType);

            switch (phase)
            {
                case MarketState.Idle:
                    return Constants.RISKS_DEMAND_MAX;

                case MarketState.Innovation:
                    return Constants.RISKS_DEMAND_MAX / 2;

                case MarketState.Trending:
                    return Constants.RISKS_DEMAND_MAX / 5;

                case MarketState.MassGrowth:
                case MarketState.MassUsage:
                    return Constants.RISKS_DEMAND_MAX / 10;

                case MarketState.Decay:
                    return Constants.RISKS_DEMAND_MAX / 2;

                case MarketState.Death:
                default:
                    return 100;
            }
        }

        // niche lifecycle
        private static int GetNichePeriodDurationInMonths(MarketState phase)
        {
            switch (phase)
            {
                case MarketState.Innovation:    return 8;
                case MarketState.Trending:      return 24;
                case MarketState.MassGrowth:    return 60;
                case MarketState.MassUsage:     return 120;
                case MarketState.Decay:         return 100;
                default: return 1;
            }
        }

        public static MarketState GetNextPhase(MarketState phase)
        {
            switch (phase)
            {
                case MarketState.Idle: return MarketState.Innovation;
                case MarketState.Innovation: return MarketState.Trending;
                case MarketState.Trending: return MarketState.MassGrowth; 
                case MarketState.MassGrowth: return MarketState.MassUsage;
                case MarketState.MassUsage: return MarketState.Decay;

                case MarketState.Decay:
                default:
                    return MarketState.Death;
            }
        }

        public static float GetMonthlyAudienceGrowthMultiplier(MarketState phase)
        {
            return 1.05f;
            switch (phase)
            {
                case MarketState.Idle:
                    return 1;

                case MarketState.Innovation:
                    return 1.01f;

                case MarketState.Trending:
                    return 1.02f;

                case MarketState.MassGrowth:
                    return 1.05f;
            }

        }

        public static int GetAudiencePercentageThatProductsWillGetDuringThisMarketState(MarketState phase)
        {
            switch (phase)
            {
                case MarketState.Innovation:    return 10;
                case MarketState.Trending:      return 25;
                case MarketState.MassGrowth:    return 55;
                case MarketState.MassUsage:     return 0;
                case MarketState.Decay:         return 10;

                case MarketState.Death:
                case MarketState.Idle:
                default:
                    return 0;
            }
        }
    }
}
