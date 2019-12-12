namespace Assets.Utils
{
    public static partial class Markets
    {
        public static long GetStartCapital(NicheType nicheType, GameContext gameContext) => GetStartCapital(GetNiche(gameContext, nicheType), gameContext);
        public static long GetStartCapital(GameEntity niche, GameContext gameContext)
        {
            var timeToMarket = Products.GetTimeToMarketFromScratch(niche);

            var timeToProfitability = 0;

            return (timeToMarket + timeToProfitability) * GetBaseProductMaintenance(gameContext, niche);
        }

        internal static long GetBaseProductMaintenance(GameContext gameContext, GameEntity niche)
        {
            var ads = 0; // GetClientAcquisitionCost(niche);
            var team = GetBiggestMaintenanceOnMarket(gameContext, niche);

            return ads + team;
        }
    }
}
