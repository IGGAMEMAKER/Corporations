namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static long GetClients(GameEntity company)
        {
            return company.marketing.clients;
        }

        public static void AddClients(GameEntity company, long clients)
        {
            var marketing = company.marketing;

            company.ReplaceMarketing(marketing.clients + clients);
        }

        public static long GetCurrentClientFlow(GameContext gameContext, NicheType nicheType)
        {
            var costs = NicheUtils.GetNicheCosts(gameContext, nicheType);

            return costs.ClientBatch / 4;
        }

        public static long GetAudienceGrowth(GameEntity product, GameContext gameContext)
        {
            var flow = GetCurrentClientFlow(gameContext, product.product.Niche);
            var multiplier = GetAudienceGrowthMultiplier(product, gameContext);

            return (long)(multiplier * flow);
        }

        public static float GetAudienceGrowthMultiplier(GameEntity product, GameContext gameContext)
        {
            var brandModifier = (3 * product.branding.BrandPower + 100) / 100;

            var conceptModifier = 1 + ProductUtils.GetDifferenceBetweenMarketDemandAndAppConcept(product, gameContext);

            return brandModifier / conceptModifier; // + rand * 50;
        }
    }
}
