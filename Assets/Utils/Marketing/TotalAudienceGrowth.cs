using Assets.Utils.Formatting;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static long GetAudienceGrowth(GameEntity product, GameContext gameContext)
        {
            var baseGrowth = GetCurrentClientFlow(gameContext, product.product.Niche) / 100;
            var clients = GetClients(product);
            var multiplier = GetAudienceGrowthMultiplier(product, gameContext);

            if (multiplier < 0)
                multiplier = 0;

            return baseGrowth + (long)(multiplier * clients) / 100;
        }
    }
}
