using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static long GetClientFlow(GameContext gameContext, NicheType nicheType)
        {
            var baseFlowForStage = GetBaseStageFlow(gameContext, nicheType);


            var stateDuration = NicheUtils.GetCurrentNicheStateDuration(gameContext, nicheType);
            var n = stateDuration;
            var q = GetMonthlyAudienceGrowthMultiplier();

            var multiplier = System.Math.Pow(n, q);



            var period = EconomyUtils.GetPeriodDuration();
            return (long)(baseFlowForStage * multiplier * period / 30);
        }

        public static float GetBaseStageFlow(GameContext gameContext, NicheType nicheType)
        {
            var costs = NicheUtils.GetNicheCosts(gameContext, nicheType);

            var niche = NicheUtils.GetNiche(gameContext, nicheType);

            var totalStageClients = costs.Audience * GetAudiencePercentageThatProductsWillGetDuringThisMarketState(niche) / 100;

            var monthlyGrowthMultiplier = GetMonthlyAudienceGrowthMultiplier();
            var stateMaxDuration = NicheUtils.GetNichePeriodDurationInMonths(niche);

            var q = monthlyGrowthMultiplier;
            var nMax = stateMaxDuration;
            var baseFlowForStage = totalStageClients * (1f - q) / (1f - Mathf.Pow(q, nMax));

            return baseFlowForStage;
        }

        public static float GetMonthlyAudienceGrowthMultiplier()
        {
            return 1.05f;
        }

        public static int GetAudiencePercentageThatProductsWillGetDuringThisMarketState(GameEntity niche) => GetAudiencePercentageThatProductsWillGetDuringThisMarketState(NicheUtils.GetMarketState(niche));
        public static int GetAudiencePercentageThatProductsWillGetDuringThisMarketState(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Innovation:
                    return 10;

                case NicheLifecyclePhase.Trending:
                    return 25;

                case NicheLifecyclePhase.MassUse:
                    return 55;

                case NicheLifecyclePhase.Decay:
                    return 10;

                case NicheLifecyclePhase.Death:
                case NicheLifecyclePhase.Idle:
                default:
                    return 0;
            }
        }

        //public static int GetPeriodDuration
    }
}
