namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static long GetAudienceGrowth(GameEntity product, GameContext gameContext)
        {
            var flow = GetClientFlow(gameContext, product.product.Niche);
            var clients = GetClients(product);

            if (clients < flow)
                clients = flow;

            var multiplier = GetAudienceGrowthMultiplier(product, gameContext);

            return multiplier * clients / 100;
        }

        public static long GetAudienceGrowthMultiplier(GameEntity product, GameContext gameContext) => GetGrowthMultiplier(product, gameContext).Sum();

        public static BonusContainer GetGrowthMultiplier(GameEntity product, GameContext gameContext)
        {
            var marketState = GetMarketStateGrowthMultiplier(product, gameContext) / 10;
            var marketingFinancing = (int)GetAudienceReachModifierBasedOnMarketingFinancing(product);

            // 0...4
            var brand = (int)product.branding.BrandPower;
            var brandModifier = GetAudienceReachBrandMultiplier(product);
            var innovationMultiplier = GetAudienceReachInnovationLeaderMultiplier(product);

            return new BonusContainer("Audience growth")
                .SetDimension("%")
                .Append("Marketing Financing", marketingFinancing)
                .Append("Market state", marketState)
                .Multiply($"Brand strength ({brand})", brandModifier)
                .MultiplyAndHideIfOne("Is Leader", innovationMultiplier)
                ;
        }

        public static int GetAudienceReachInnovationLeaderMultiplier(GameEntity product) => product.isTechnologyLeader ? 2 : 1;
        public static int GetAudienceReachBrandMultiplier (GameEntity product)
        {
            // 0...4
            var brand = (int)product.branding.BrandPower;
            return (3 * brand + 100) / 100;
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
        public static float GetAudienceReachModifierBasedOnMarketingFinancing(GameEntity product) => GetAudienceReachModifierBasedOnMarketingFinancing(product.financing.Financing[Financing.Marketing]);
        public static float GetAudienceReachModifierBasedOnMarketingFinancing(int financing)
        {
            switch (financing)
            {
                case 0: return 1;
                case 1: return 2;
                case 2: return 5;
                case 3: return 10;
                default: return 10000;
            }
        }
    }
}
