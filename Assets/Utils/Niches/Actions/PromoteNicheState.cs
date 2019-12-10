namespace Assets.Utils
{
    public static partial class Markets
    {
        // update
        public static void PromoteNicheState(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            var next = GetNextPhase(phase);

            var newDuration = GetNichePeriodDurationInMonths(next);
            niche.ReplaceNicheState(next, newDuration);
        }
    }
}
