namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static long GetStartCapital(NicheType nicheType, GameContext gameContext) => GetStartCapital(GetNiche(gameContext, nicheType));
        public static long GetStartCapital(GameEntity niche)
        {
            var marketDemand = ProductUtils.GetMarketDemand(niche);
            var iterationTime = ProductUtils.GetBaseIterationTime(niche);

            var timeToMarket = marketDemand * iterationTime / 30;

            var timeToProfitability = 0;

            return (timeToMarket + timeToProfitability) * GetBaseProductMaintenance(niche);
        }

        internal static long GetBaseProductMaintenance(GameEntity niche)
        {
            var ads = 0; // GetClientAcquisitionCost(niche);
            var team = GetBaseDevelopmentCost(niche);

            return ads + team;
        }
    }
}
