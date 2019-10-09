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

        public static NicheCostsComponent GetNicheCosts(GameEntity niche)
        {
            var state = GetMarketState(niche);

            var costModifier = GetMarketStateCostsModifier(state);
            var flowModifier = GetMarketStateClientFlowModifier(state);
            var priceModifier = GetMarketStatePriceModifier(state);

            var costs = niche.nicheCosts;

            var component = new NicheCostsComponent
            {
                BasePrice       = costs.BasePrice * priceModifier,
                AdCost          = costs.AdCost * costModifier,
                ClientBatch     = costs.ClientBatch * flowModifier,
                
                IdeaCost        = costs.IdeaCost * costModifier,
                TechCost        = costs.TechCost * costModifier,
                MarketingCost   = costs.MarketingCost * costModifier, // 
            };

            return component;
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
            var marketDemand = ProductUtils.GetMarketDemand(null, niche);
            var iterationTime = ProductUtils.GetBaseIterationTime(niche);

            var timeToMarket = marketDemand * iterationTime / 30;

            var timeToProfitability = 24;

            return (timeToMarket + timeToProfitability) * GetBaseProductMaintenance(niche);
        }

        // Maintenance
        internal static long GetBaseProductMaintenance(GameEntity niche)
        {
            var costs = GetNicheCosts(niche);

            var ads = costs.AdCost;
            var team = costs.TechCost;

            return ads + team;
        }
    }
}
