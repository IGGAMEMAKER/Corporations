using UnityEngine;

namespace Assets.Core
{
    public static partial class Markets
    {
        public static NicheCostsComponent GetNicheCosts(GameContext context, NicheType nicheType) => GetNicheCosts(Get(context, nicheType));
        public static NicheCostsComponent GetNicheCosts(GameEntity niche)
        {
            var costs = niche.nicheCosts;
            var state = GetMarketState(niche);

            var adModifier    = GetMarketStateAdCostModifier(state);

            return new NicheCostsComponent
            {
                BaseIncome       = costs.BaseIncome,
                AcquisitionCost  = costs.AcquisitionCost * adModifier,
                TechCost         = costs.TechCost,

                Audience         = costs.Audience,
            };
        }

        // base marketing cost
        public static float GetClientAcquisitionCost(NicheType nicheType, GameContext gameContext) => GetClientAcquisitionCost(Get(gameContext, nicheType));
        public static float GetClientAcquisitionCost(GameEntity niche)
        {
            var costs = GetNicheCosts(niche);

            return costs.AcquisitionCost;
        }

        // flow
        public static long GetClientFlow(GameContext gameContext, NicheType nicheType)
        {
            var niche = Get(gameContext, nicheType);
            var baseFlowForStage = GetBaseStageFlow(gameContext, niche, nicheType);


            var stateDuration = GetCurrentNicheStateDuration(gameContext, nicheType);

            var n = stateDuration + 1;
            var q = GetMonthlyAudienceGrowthMultiplier(niche);

            var multiplier = Mathf.Pow(q, n);

            var result = baseFlowForStage * multiplier;

            // normalise
            return (long)(result * Balance.PERIOD / 30);
        }

        public static float GetBaseStageFlow(GameContext gameContext, GameEntity niche, NicheType nicheType)
        {
            var costs = GetNicheCosts(gameContext, nicheType);


            var totalStageClients = costs.Audience * GetAudiencePercentageThatProductsWillGetDuringThisMarketState(niche) / 100;

            var monthlyGrowthMultiplier = GetMonthlyAudienceGrowthMultiplier(niche);
            var stateMaxDuration = GetNichePeriodDurationInMonths(niche);

            var q = monthlyGrowthMultiplier;
            var nMax = stateMaxDuration;



            return totalStageClients * (1f - q) / (1f - Mathf.Pow(q, nMax));
        }

        public static float GetMonthlyAudienceGrowthMultiplier(GameEntity niche) => GetMonthlyAudienceGrowthMultiplier(GetMarketState(niche));

        public static int GetAudiencePercentageThatProductsWillGetDuringThisMarketState(GameEntity niche) => GetAudiencePercentageThatProductsWillGetDuringThisMarketState(GetMarketState(niche));
    }
}
