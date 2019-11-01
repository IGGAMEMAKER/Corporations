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

            var financing = product.financing.Financing[Financing.Marketing];
            float financingModifier = GetAudienceReachModifierBasedOnFinancing(financing);


            return brandModifier * financingModifier / conceptModifier; // + rand * 50;
        }

        public static float GetAudienceViralGrowthModifier(GameEntity product, GameContext gameContext)
        {
            return 0.5f * product.branding.BrandPower / 100;
        }

        public static float GetAudienceReachModifierBasedOnFinancing(int financing)
        {
            switch (financing)
            {
                case 0: return 0.5f;
                case 1: return 1f;
                case 2: return 2f;
                case 3: return 5f;
                default: return 10000;
            }
        }
        public static float GetMarketingStrengthBasedOnFinancing(int financing)
        {
            switch (financing)
            {
                case 0: return 0;
                case 1: return 1f;
                case 2: return 15;
                case 3: return 25;
                default: return 10000;
            }
        }
    }
}
