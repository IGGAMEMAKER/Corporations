namespace Assets.Utils
{
    public static partial class Markets
    {
        // update
        public static void UpdateNicheDuration(GameEntity niche)
        {
            var phase = GetMarketState(niche);
            var newDuration = GetNicheDuration(niche);
            
            niche.ReplaceNicheState(phase, newDuration);
        }

        // get
        public static int GetNicheDuration(GameEntity niche) => GetNichePeriodDurationInMonths(niche);

        public static int GetNichePeriodDurationInMonths(GameEntity niche) => GetNichePeriodDurationInMonths(GetMarketState(niche));
        private static int GetNichePeriodDurationInMonths(NicheState phase)
        {
            switch (phase)
            {
                case NicheState.Innovation:    return 8;
                case NicheState.Trending:      return 24;
                case NicheState.MassGrowth:    return 60;
                case NicheState.Decay:         return 100;
                default: return 1;
            }
        }

        public static int GetCurrentNicheStateDuration(GameContext gameContext, NicheType nicheType) => GetCurrentNicheStateDuration(GetNiche(gameContext, nicheType));
        public static int GetCurrentNicheStateDuration(GameEntity niche)
        {
            var stateDuration = GetNichePeriodDurationInMonths(niche) - niche.nicheState.Duration;

            return stateDuration;
        }

        public static NicheState GetNextPhase(NicheState phase)
        {
            switch (phase)
            {
                case NicheState.Idle:
                    return NicheState.Innovation;

                case NicheState.Innovation:
                    return NicheState.Trending;

                case NicheState.Trending:
                    return NicheState.MassGrowth;

                case NicheState.MassGrowth:
                    return NicheState.Decay;

                case NicheState.Decay:
                default:
                    return NicheState.Death;
            }
        }

        public static float GetMonthlyAudienceGrowthMultiplier(GameEntity niche) => GetMonthlyAudienceGrowthMultiplier(Markets.GetMarketState(niche));
        public static float GetMonthlyAudienceGrowthMultiplier(NicheState phase)
        {
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

            return 1.05f;
        }

        public static int GetAudiencePercentageThatProductsWillGetDuringThisMarketState(GameEntity niche) => GetAudiencePercentageThatProductsWillGetDuringThisMarketState(Markets.GetMarketState(niche));
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
