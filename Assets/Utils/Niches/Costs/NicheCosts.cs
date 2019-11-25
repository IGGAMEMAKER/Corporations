using UnityEngine;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static NicheCostsComponent GetNicheCosts(GameContext context, NicheType nicheType) => GetNicheCosts(GetNiche(context, nicheType));
        public static NicheCostsComponent GetNicheCosts(GameEntity niche)
        {
            var costs = niche.nicheCosts;
            var state = GetMarketState(niche);

            var flowModifier  = GetClientFlowModifier(niche);

            var priceModifier = GetMarketStatePriceModifier(state);
            var adModifier    = GetMarketStateAdCostModifier(state);

            return new NicheCostsComponent
            {
                BaseIncome       = costs.BaseIncome * priceModifier,
                AcquisitionCost  = costs.AcquisitionCost * adModifier,
                TechCost         = costs.TechCost,

                ClientBatch      = (int)(costs.ClientBatch * flowModifier),
            };
        }

        public static float GetClientFlowModifier(GameEntity niche)
        {
            var state = GetMarketState(niche);

            if (state == NicheLifecyclePhase.Idle || state == NicheLifecyclePhase.Death)
                return 0;

            NicheLifecyclePhase phase = NicheLifecyclePhase.Innovation;

            float modifier = 0;
            while (phase != state)
            {
                var duration = GetNichePeriodDurationInMonths(phase);

                modifier += Mathf.Pow(GetMarketStateClientFlowModifier(phase), duration);

                phase = GetNextPhase(phase);
            }

            var maxDuration = GetNichePeriodDurationInMonths(state);
            var nicheDuration = niche.nicheState.Duration;
            var x = maxDuration - nicheDuration;
            modifier += Mathf.Pow(GetMarketStateClientFlowModifier(state), x);

            Debug.Log(" Sum modifier =" + modifier);

            return modifier;
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
    }
}
