namespace Assets.Utils
{
    public static partial class NicheUtils
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
        public static int GetNichePeriodDurationInMonths(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Innovation: return 8;
                case NicheLifecyclePhase.Trending:   return 24;
                case NicheLifecyclePhase.MassUse:    return 60;
                case NicheLifecyclePhase.Decay:      return 100;
                default: return 1;
            }
        }

        public static int GetCurrentNicheStateDuration(GameContext gameContext, NicheType nicheType) => GetCurrentNicheStateDuration(GetNiche(gameContext, nicheType));
        public static int GetCurrentNicheStateDuration(GameEntity niche)
        {
            var stateDuration = GetNichePeriodDurationInMonths(niche) - niche.nicheState.Duration;

            return stateDuration;
        }

        public static NicheLifecyclePhase GetNextPhase(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Idle:
                    return NicheLifecyclePhase.Innovation;

                case NicheLifecyclePhase.Innovation:
                    return NicheLifecyclePhase.Trending;

                case NicheLifecyclePhase.Trending:
                    return NicheLifecyclePhase.MassUse;

                case NicheLifecyclePhase.MassUse:
                    return NicheLifecyclePhase.Decay;

                case NicheLifecyclePhase.Decay:
                default:
                    return NicheLifecyclePhase.Death;
            }
        }
    }
}
