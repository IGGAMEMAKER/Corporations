using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static long GetClientFlow(GameContext gameContext, NicheType nicheType)
        {
            var niche = NicheUtils.GetNiche(gameContext, nicheType);
            var baseFlowForStage = GetBaseStageFlow(gameContext, niche, nicheType);


            var stateDuration = NicheUtils.GetCurrentNicheStateDuration(gameContext, nicheType);
            var n = stateDuration + 1;
            var q = GetMonthlyAudienceGrowthMultiplier(niche);

            var multiplier = Mathf.Pow(q, n);

            var result = baseFlowForStage * multiplier;

            // normalise
            var period = EconomyUtils.GetPeriodDuration();
            return (long)(result * period / 30);
        }

        public static float GetBaseStageFlow(GameContext gameContext, GameEntity niche, NicheType nicheType)
        {
            var costs = NicheUtils.GetNicheCosts(gameContext, nicheType);


            var totalStageClients = costs.Audience * GetAudiencePercentageThatProductsWillGetDuringThisMarketState(niche) / 100;

            var monthlyGrowthMultiplier = GetMonthlyAudienceGrowthMultiplier(niche);
            var stateMaxDuration = NicheUtils.GetNichePeriodDurationInMonths(niche);

            var q = monthlyGrowthMultiplier;
            var nMax = stateMaxDuration;
            var baseFlowForStage = totalStageClients * (1f - q) / (1f - Mathf.Pow(q, nMax));

            return baseFlowForStage;
        }

        public static float GetMonthlyAudienceGrowthMultiplier(GameEntity niche) => GetMonthlyAudienceGrowthMultiplier(NicheUtils.GetMarketState(niche));
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

        public static int GetAudiencePercentageThatProductsWillGetDuringThisMarketState(GameEntity niche) => GetAudiencePercentageThatProductsWillGetDuringThisMarketState(NicheUtils.GetMarketState(niche));
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

        //public static int GetPeriodDuration
    }
}
