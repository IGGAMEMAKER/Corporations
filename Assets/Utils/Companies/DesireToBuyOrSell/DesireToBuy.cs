namespace Assets.Utils
{
    public static partial class Companies
    {
        public static long GetDesireToBuy(GameEntity buyer, GameEntity target, GameContext gameContext)
        {
            if (buyer.isManagingCompany && target.hasProduct)
                return GetDesireToBuyStartupAsGroup(gameContext, buyer, target);

            return 0;
        }

        public static bool IsWillBuyCompany(GameEntity buyer, GameEntity target, GameContext gameContext)
        {
            return GetDesireToBuy(buyer, target, gameContext) > 0;
        }

        public static long GetStartupAttractiveness(GameContext gameContext, GameEntity startup)
        {
            long score = 0;

            if (Markets.IsPlayableNiche(gameContext, startup.product.Niche))
                score += 100;
            else
                score -= 100;

            var positionOnMarket = Markets.GetPositionOnMarket(gameContext, startup) + 1;
            score += 100 / positionOnMarket;

            var quality = Markets.GetAppQualityOnMarket(gameContext, startup) + 1;
            score += 30 / quality;

            score += (long)startup.branding.BrandPower;

            return score;
        }

        public static long GetDesireToBuyStartupAsGroup(GameContext gameContext, GameEntity group, GameEntity startup)
        {
            long score = 0;

            if (IsInSphereOfInterest(group, startup))
                score += 1000;

            var attractiveness = GetStartupAttractiveness(gameContext, startup);

            return score;
        }
    }
}
