namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static int GetMarketStateCostsModifier(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Innovation:    return 1;
                case NicheLifecyclePhase.Trending:      return 5;
                case NicheLifecyclePhase.MassUse:       return 12;
                case NicheLifecyclePhase.Decay:         return 7;

                default: return 0;
            }
        }

        public static int GetMarketStateClientFlowModifier(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Innovation:    return 1;
                case NicheLifecyclePhase.Trending:      return 7;
                case NicheLifecyclePhase.MassUse:       return 10;
                case NicheLifecyclePhase.Decay:         return 1;

                default: return 0;
            }
        }

        public static float GetMarketStatePriceModifier(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Innovation:    return 1;
                case NicheLifecyclePhase.Trending:      return 0.8f;
                case NicheLifecyclePhase.MassUse:       return 0.7f;
                case NicheLifecyclePhase.Decay:         return 0.6f;

                default: return 0;
            }
        }

        public static float GetMarketStateAdCostModifier(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Innovation:    return 0.1f;
                case NicheLifecyclePhase.Trending:      return 0.5f;
                case NicheLifecyclePhase.MassUse:       return 1f;
                case NicheLifecyclePhase.Decay:         return 2f;

                default: return 0;
            }
        }

        public static NicheCostsComponent GetNicheCosts(GameEntity niche)
        {
            var state = GetMarketState(niche);

            var flowModifier = GetMarketStateClientFlowModifier(state);
            var priceModifier = GetMarketStatePriceModifier(state);
            var adModifier = GetMarketStateAdCostModifier(state);

            var costs = niche.nicheCosts;

            return new NicheCostsComponent
            {
                BaseIncome       = costs.BaseIncome * priceModifier,
                AdCost          = (int)(costs.AdCost * adModifier),
                TechCost        = costs.TechCost,

                ClientBatch     = costs.ClientBatch * flowModifier,
                
                //IdeaCost        = costs.IdeaCost * costModifier,
                //MarketingCost   = costs.MarketingCost * costModifier, // 
            };
        }

        public static NicheCostsComponent GetNicheCosts(GameContext context, NicheType nicheType)
        {
            var niche = GetNicheEntity(context, nicheType);

            return GetNicheCosts(niche);
        }

        
        // Start capital
        public static long GetStartCapital(NicheType nicheType, GameContext gameContext)
        {
            var niche = GetNicheEntity(gameContext, nicheType);

            return GetStartCapital(niche);
        }
        public static long GetStartCapital(GameEntity niche)
        {
            var marketDemand = ProductUtils.GetMarketDemand(niche);
            var iterationTime = ProductUtils.GetBaseIterationTime(niche);

            var timeToMarket = marketDemand * iterationTime / 30;

            var timeToProfitability = 0;

            return (timeToMarket + timeToProfitability) * GetBaseProductMaintenance(niche);
        }


        // base marketing cost
        public static long GetBaseMarketingCost(GameEntity niche)
        {
            var costs = GetNicheCosts(niche);

            return costs.AdCost;
        }
        public static long GetBaseMarketingCost(NicheType nicheType, GameContext gameContext)
        {
            var niche = GetNicheEntity(gameContext, nicheType);

            return GetBaseMarketingCost(niche);
        }

        // base development cost
        public static long GetBaseDevelopmentCost(GameEntity niche)
        {
            var costs = GetNicheCosts(niche);

            return costs.TechCost * Constants.SALARIES_PROGRAMMER;
        }
        public static long GetBaseDevelopmentCost(NicheType nicheType, GameContext gameContext)
        {
            var niche = GetNicheEntity(gameContext, nicheType);

            return GetBaseDevelopmentCost(niche);
        }


        // Maintenance
        internal static long GetBaseProductMaintenance(GameEntity niche)
        {
            var ads = GetBaseMarketingCost(niche);
            var team = GetBaseDevelopmentCost(niche);

            return ads + team;
        }
    }
}
