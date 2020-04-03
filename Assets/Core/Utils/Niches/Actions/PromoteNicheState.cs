using UnityEngine;

namespace Assets.Core
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

        public static void DecrementDuration(GameEntity niche)
        {
            var state = niche.nicheState;

            niche.ReplaceNicheState(state.Phase, Mathf.Max(state.Duration - 1, 0));
        }
    }
}
