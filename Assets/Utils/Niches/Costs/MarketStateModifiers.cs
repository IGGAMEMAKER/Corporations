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
                case NicheState.MassGrowth:       return 0.7f;
                case NicheState.Decay:         return 0.6f;

                default: return 0;
            }
        }

        public static float GetMarketStateAdCostModifier(NicheState phase)
        {
            switch (phase)
            {
                case NicheState.Innovation:    return 0.1f;
                case NicheState.Trending:      return 0.5f;
                case NicheState.MassGrowth:       return 1f;
                case NicheState.Decay:         return 2f;

                default: return 0;
            }
        }
    }
}
