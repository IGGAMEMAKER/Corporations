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

            var period = EconomyUtils.GetPeriodDuration();

            return costs.ClientBatch * period / 30;
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
            var improvements = (100 + product.productImprovements.Improvements[ProductImprovement.Acquisition] * 2) / 100;

            return brandModifier * improvements / conceptModifier; // + rand * 50;
        }
    }
}
