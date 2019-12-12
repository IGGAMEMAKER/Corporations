using UnityEngine;

namespace Assets.Utils
{
    public static partial class Markets
    {
        public static NicheCostsComponent GetNicheCosts(GameContext context, NicheType nicheType) => GetNicheCosts(GetNiche(context, nicheType));
        public static NicheCostsComponent GetNicheCosts(GameEntity niche)
        {
            var costs = niche.nicheCosts;
            var state = GetMarketState(niche);

            var priceModifier = GetMarketStatePriceModifier(state);
            var adModifier    = GetMarketStateAdCostModifier(state);

            return new NicheCostsComponent
            {
                BaseIncome       = costs.BaseIncome * priceModifier,
                AcquisitionCost  = costs.AcquisitionCost * adModifier,
                TechCost         = costs.TechCost,

                Audience         = costs.Audience,
            };
        }

        // base marketing cost
        public static float GetClientAcquisitionCost(NicheType nicheType, GameContext gameContext) => GetClientAcquisitionCost(GetNiche(gameContext, nicheType));
        public static float GetClientAcquisitionCost(GameEntity niche)
        {
            var costs = GetNicheCosts(niche);

            return costs.AcquisitionCost;
        }

        // base development cost
        public static long GetBaseDevelopmentCost(NicheType nicheType, GameContext gameContext) => GetBaseDevelopmentCost(GetNiche(gameContext, nicheType));
        public static long GetBaseDevelopmentCost(GameEntity niche)
        {
            var costs = GetNicheCosts(niche);

            return costs.TechCost * Constants.SALARIES_PROGRAMMER;
        }

        // flow
        public static long GetClientFlow(GameContext gameContext, NicheType nicheType)
        {
            var niche = GetNiche(gameContext, nicheType);
            var baseFlowForStage = GetBaseStageFlow(gameContext, niche, nicheType);


            var stateDuration = GetCurrentNicheStateDuration(gameContext, nicheType);
            var n = stateDuration + 1;
            var q = GetMonthlyAudienceGrowthMultiplier(niche);

            var multiplier = Mathf.Pow(q, n);

            var result = baseFlowForStage * multiplier;

            // normalise
            var period = Economy.GetPeriodDuration();
            return (long)(result * period / 30);
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
    }
}
