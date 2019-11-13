using Assets.Utils.Formatting;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static float GetAudienceGrowthMultiplier(GameEntity product, GameContext gameContext)
        {
            var bonus = GetGrowthMultiplier(product, gameContext);

            return bonus.Sum();
        }

        public static BonusContainer GetGrowthMultiplier(GameEntity product, GameContext gameContext)
        {
            var marketGrowthMultiplier = GetMarketStateGrowthMultiplier(product, gameContext);

            // 0...4
            var brand = (int)product.branding.BrandPower;
            var brandModifier = 3 * brand + 100;

            var marketingModifier = GetAudienceReachModifierBasedOnMarketingFinancing(product) * 3;

            return new BonusContainer("Audience growth")
                .SetDimension("%")
                .Append("Marketing Financing", (int)marketingModifier)
                .Append($"Brand strength ({brand})", brandModifier / 100)
                .Append("Market state", marketGrowthMultiplier)
                ;
        }

        public static long GetAudienceGrowth(GameEntity product, GameContext gameContext)
        {
            var baseGrowth = GetCurrentClientFlow(gameContext, product.product.Niche) / 100;
            var clients = GetClients(product);
            var multiplier = GetAudienceGrowthMultiplier(product, gameContext);

            if (multiplier < 0)
                multiplier = 0;

            return baseGrowth + (long)(multiplier * clients) / 100;
        }



        // based on market state
        public static int GetGrowthMultiplierBasedOnMonetisationType(GameEntity niche)
        {
            var profile = niche.nicheBaseProfile.Profile;


            var monetisationType = profile.MonetisationType;
            var baseGrowth = 5; // 5%
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

            return baseGrowth;
        }

        public static int GetGrowthMultiplierBasedOnMarketState(GameEntity niche)
        {
            var marketStage = NicheUtils.GetMarketState(niche);
            var marketStageGrowth = 0;
            switch (marketStage)
            {
                case NicheLifecyclePhase.Innovation: marketStageGrowth = 10; break;
                case NicheLifecyclePhase.Trending: marketStageGrowth = 6; break;

                case NicheLifecyclePhase.MassUse: marketStageGrowth = 2; break;
                case NicheLifecyclePhase.Decay: marketStageGrowth = 1; break;
            }

            return marketStageGrowth;
        }

        public static int GetMarketStateGrowthMultiplier(GameEntity product, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            var baseGrowth = GetGrowthMultiplierBasedOnMonetisationType(niche);
            var marketStageGrowth = GetGrowthMultiplierBasedOnMarketState(niche);

            //.Append("Market state " + marketStage.ToString(), baseGrowth)
            //.Append("Monetisation type " + monetisationType.ToString(), +marketStageGrowth)

            return (baseGrowth + marketStageGrowth);
        }

        // based on financing
        public static float GetAudienceReachModifierBasedOnMarketingFinancing(GameEntity product)
        {
            var financing = product.financing.Financing[Financing.Marketing];

            return GetAudienceReachModifierBasedOnMarketingFinancing(financing);
        }
        public static float GetAudienceReachModifierBasedOnMarketingFinancing(int financing)
        {
            switch (financing)
            {
                case 0: return 0.5f;
                case 1: return 1f;
                case 2: return 2.8f;
                case 3: return 3.5f;
                default: return 10000;
            }
        }
    }
}
