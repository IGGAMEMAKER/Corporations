namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static NicheCostsComponent GetNicheCosts(GameContext context, NicheType nicheType) => GetNicheCosts(GetNiche(context, nicheType));
        public static NicheCostsComponent GetNicheCosts(GameEntity niche)
        {
            var costs = niche.nicheCosts;
            var state = GetMarketState(niche);

            var priceModifier = GetMarketStatePriceModifier(state);
            var flowModifier  = GetMarketStateClientFlowModifier(state);
            var adModifier    = GetMarketStateAdCostModifier(state);

            return new NicheCostsComponent
            {
                BaseIncome       = costs.BaseIncome * priceModifier,
                AcquisitionCost  = (int)(costs.AcquisitionCost * adModifier),
                TechCost         = costs.TechCost,

                ClientBatch      = costs.ClientBatch * flowModifier,
                
                //IdeaCost       = costs.IdeaCost * costModifier,
                //MarketingCost  = costs.MarketingCost * costModifier, // 
            };
        }



        // base marketing cost
        public static long GetClientAcquisitionCost(NicheType nicheType, GameContext gameContext) => GetClientAcquisitionCost(GetNiche(gameContext, nicheType));
        public static long GetClientAcquisitionCost(GameEntity niche)
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
