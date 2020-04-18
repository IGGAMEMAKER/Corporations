namespace Assets.Core
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
        // same
        private static int GetNichePeriodDurationInMonths(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            if (phase == MarketState.MassUsage)
                return GetMassUsageDuration(niche);

            return GetNichePeriodDurationInMonths(phase);
        }

        private static int GetNichePeriodDurationInMonths(
            MarketState phase,
            bool isFastGrowingOnStart = false, bool isSlowlyDecaying = false)
        {
            switch (phase)
            {
                case MarketState.Innovation: return 8;
                case MarketState.Trending: return 24;
                case MarketState.MassGrowth: return 60;
                //case MarketState.MassUsage: return 
                case MarketState.Decay: return 24;
                default: return 1;
            }
        }

        public static int GetCurrentNicheStateDuration(GameContext gameContext, NicheType nicheType) => GetCurrentNicheStateDuration(Get(gameContext, nicheType));
        public static int GetCurrentNicheStateDuration(GameEntity niche)
        {
            var stateDuration = GetNichePeriodDurationInMonths(niche) - niche.nicheState.Duration;

            return stateDuration;
        }

        private static int GetMassUsageDuration(GameEntity niche)
        {
            var lifecycle = niche.nicheLifecycle;

            var startDate = lifecycle.OpenDate;
            var endDate = lifecycle.EndDate;

            var idle = GetNichePeriodDurationInMonths(MarketState.Idle);
            var innovation = GetNichePeriodDurationInMonths(MarketState.Innovation);
            var trending = GetNichePeriodDurationInMonths(MarketState.Trending);
            var massGrowth = GetNichePeriodDurationInMonths(MarketState.MassGrowth);
            var decay = GetNichePeriodDurationInMonths(MarketState.Decay);

            var wholeLifetime = (endDate - startDate) / 30; // months

            return wholeLifetime - idle - innovation - trending - massGrowth - decay;
        }
    }
}
