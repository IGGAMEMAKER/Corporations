namespace Assets.Utils
{
    public static partial class Markets
    {
        public static long GetStartCapital(NicheType nicheType, GameContext gameContext) => GetStartCapital(GetNiche(gameContext, nicheType), gameContext);
        public static long GetStartCapital(GameEntity niche, GameContext gameContext)
        {
            var timeToMarket = Products.GetTimeToMarketFromScratch(niche);

            var timeToProfitability = timeToMarket / 2;

            var buffer = 15000;

            return (timeToMarket + timeToProfitability) * GetBaseProductMaintenance(gameContext, niche) + buffer;
        }

        internal static long GetBaseProductMaintenance(GameContext gameContext, GameEntity niche)
        {
            return GetBiggestMaintenanceOnMarket(gameContext, niche);
        }
    }
}
