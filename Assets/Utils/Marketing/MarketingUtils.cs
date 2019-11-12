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
            //var flow = GetCurrentClientFlow(gameContext, product.product.Niche);
            var clients = GetClients(product);
            var multiplier = GetAudienceGrowthMultiplier(product, gameContext);

            return (long)(multiplier * clients) / 100;
        }

        public static float GetAudienceGrowthMultiplier(GameEntity product, GameContext gameContext)
        {
            //var brandModifier = (3 * product.branding.BrandPower + 100) / 100;
            //var conceptModifier = 1 + ProductUtils.GetDifferenceBetweenMarketDemandAndAppConcept(product, gameContext);

            //float financingModifier = GetAudienceReachModifierBasedOnFinancing(product);
            //float productPhaseModigier = GetAudienceReachModifierBasedOnDevelopmentFinancing(product);

            //return brandModifier * financingModifier * productPhaseModigier / conceptModifier; // + rand * 50;

            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);
            var profile = niche.nicheBaseProfile.Profile;
            var monetisationType = profile.MonetisationType;
            var marketStage = NicheUtils.GetMarketState(niche);

            var baseGrowth = 5f; // 5%

            switch (monetisationType)
            {
                case Monetisation.Adverts:
                    baseGrowth = 10;
                    break;
                case Monetisation.Enterprise:
                case Monetisation.Service:
                    baseGrowth = 3;
                    break;

                case Monetisation.Paid:
                    baseGrowth = 1;
                    break;
            }

            var marketStageGrowth = 0f;
            switch (marketStage)
            {
                case NicheLifecyclePhase.Innovation: marketStageGrowth = 5; break;
                case NicheLifecyclePhase.Trending: marketStageGrowth = 3; break;

                case NicheLifecyclePhase.MassUse: marketStageGrowth = 1; break;
                case NicheLifecyclePhase.Decay: marketStageGrowth = 0.5f; break;
            }



            // 0...4
            var brandModifier = (3 * product.branding.BrandPower + 100) / 100;
            // 1...888
            var conceptModifier = 1 + ProductUtils.GetDifferenceBetweenMarketDemandAndAppConcept(product, gameContext);
            var innovationBonus = product.isTechnologyLeader ? 2 : 1;

            float financingModifier = GetAudienceReachModifierBasedOnFinancing(product);
            float productSizeModifier = GetAudienceReachModifierBasedOnDevelopmentFinancing(product);

            return baseGrowth * marketStageGrowth * innovationBonus * brandModifier * financingModifier * productSizeModifier / conceptModifier; // + rand * 50;
        }



        public static float GetAudienceReachModifierBasedOnFinancing(GameEntity product)
        {
            var financing = product.financing.Financing[Financing.Marketing];

            return GetAudienceReachModifierBasedOnFinancing(financing);
        }
        public static float GetAudienceReachModifierBasedOnFinancing(int financing)
        {
            switch (financing)
            {
                case 0: return 0.5f;
                case 1: return 1f;
                case 2: return 2.8f;
                default: return 10000;
            }
        }



        public static float GetAudienceReachModifierBasedOnDevelopmentFinancing(GameEntity product)
        {
            var financing = product.financing.Financing[Financing.Development];

            return GetAudienceReachModifierBasedOnDevelopmentFinancing(financing);
        }
        public static float GetAudienceReachModifierBasedOnDevelopmentFinancing(int financing)
        {
            switch (financing)
            {
                case 0: return 0.5f;
                case 1: return 1f;
                case 2: return 2f;
                case 3: return 3f;
                default: return 10000;
            }
        }
    }
}
