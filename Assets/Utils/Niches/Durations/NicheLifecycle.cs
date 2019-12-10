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

        public static int GetCurrentNicheStateDuration(GameContext gameContext, NicheType nicheType) => GetCurrentNicheStateDuration(GetNiche(gameContext, nicheType));
        public static int GetCurrentNicheStateDuration(GameEntity niche)
        {
            var stateDuration = GetNichePeriodDurationInMonths(niche) - niche.nicheState.Duration;

            return stateDuration;
        }

        public static float GetMonthlyAudienceGrowthMultiplier(GameEntity niche) => GetMonthlyAudienceGrowthMultiplier(Markets.GetMarketState(niche));

        public static int GetAudiencePercentageThatProductsWillGetDuringThisMarketState(GameEntity niche) => GetAudiencePercentageThatProductsWillGetDuringThisMarketState(Markets.GetMarketState(niche));
    }
}
