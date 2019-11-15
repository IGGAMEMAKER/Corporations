namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static long GetAudienceGrowth(GameEntity product, GameContext gameContext)
        {
            var baseGrowth = GetCurrentClientFlow(gameContext, product.product.Niche) / 1000;
            if (baseGrowth == 0)
                baseGrowth = 1;

            var clients = GetClients(product);
            var multiplier = GetAudienceGrowthMultiplier(product, gameContext);

            return (long)(multiplier * clients) / 100;
        }

        public static float GetAudienceGrowthMultiplier(GameEntity product, GameContext gameContext)
        {
            return GetGrowthMultiplier(product, gameContext).Sum();
        }

        public static BonusContainer GetGrowthMultiplier(GameEntity product, GameContext gameContext)
        {
            var marketGrowthMultiplier = GetMarketStateGrowthMultiplier(product, gameContext) / 10;

            // 0...4
            var brand = (int)product.branding.BrandPower;
            var brandModifier = 3 * brand + 100;

            var marketingModifier = (int)GetAudienceReachModifierBasedOnMarketingFinancing(product) * 2;

            return new BonusContainer("Audience growth")
                .SetDimension("%")
                .Append("Marketing Financing", marketingModifier)
                .Append($"Brand strength ({brand})", brandModifier / 100)
                .Append("Market state", marketGrowthMultiplier)
                ;
        }



        // based on market state
        public static int GetMarketStateGrowthMultiplier(GameEntity product, GameContext gameContext)
        {
            var niche = NicheUtils.GetNiche(gameContext, product.product.Niche);

            var baseGrowth = GetGrowthMultiplierBasedOnMonetisationType(niche);
            var marketStageGrowth = GetGrowthMultiplierBasedOnMarketState(niche);

            return baseGrowth + marketStageGrowth;
        }

        public static int GetGrowthMultiplierBasedOnMonetisationType(GameEntity niche)
        {
            switch (niche.nicheBaseProfile.Profile.MonetisationType)
            {
                case Monetisation.Adverts: return 10;

                case Monetisation.Enterprise:
                case Monetisation.Service: return 3;

                case Monetisation.Paid: return 1;
                default: return 5;
            }
        }

        public static int GetGrowthMultiplierBasedOnMarketState(GameEntity niche)
        {
            switch (NicheUtils.GetMarketState(niche))
            {
                case NicheLifecyclePhase.Innovation: return 10;
                case NicheLifecyclePhase.Trending: return 6;

                case NicheLifecyclePhase.MassUse: return 2;
                case NicheLifecyclePhase.Decay: return 1;
                default: return 0;
            }
        }

        // based on financing
        public static float GetAudienceReachModifierBasedOnMarketingFinancing(int financing)
        {
            switch (financing)
            {
                case 0: return 1;
                case 1: return 3;
                case 2: return 4;
                case 3: return 6;
                default: return 10000;
            }
        }

        public static float GetAudienceReachModifierBasedOnMarketingFinancing(GameEntity product)
        {
            var financing = product.financing.Financing[Financing.Marketing];

            return GetAudienceReachModifierBasedOnMarketingFinancing(financing);
        }
    }
}
