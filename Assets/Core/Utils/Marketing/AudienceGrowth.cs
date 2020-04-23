using System.Linq;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static float GetSumOfBrandPowers(NicheType nicheType, GameContext gameContext) => GetSumOfBrandPowers(Markets.Get(gameContext, nicheType), gameContext);
        public static float GetSumOfBrandPowers(GameEntity niche, GameContext gameContext)
        {
            var products = Markets.GetProductsOnMarket(niche, gameContext);

            var sumOfBrandPowers = products.Sum(p => p.branding.BrandPower);

            return sumOfBrandPowers;
        }

        public static float GetBrandBasedMarketShare(GameEntity e, GameContext gameContext)
        {
            var niche = Markets.Get(gameContext, e);

            var sumOfBrandPowers = GetSumOfBrandPowers(niche, gameContext);

            // +1 : avoid division by zero
            return e.branding.BrandPower / (sumOfBrandPowers + 1);
        }

        public static long GetBrandBasedAudienceGrowth(GameEntity e, GameContext gameContext)
        {
            var brandBasedMarketShare = GetBrandBasedMarketShare(e, gameContext);

            var flow = GetClientFlow(gameContext, e.product.Niche);

            return (long)(brandBasedMarketShare * flow);
        }

        public static long GetTargetingCampaignGrowth3(GameEntity e, GameContext gameContext)
        {
            return GetBrandBasedAudienceGrowth(e, gameContext) * 10;
        }
        public static long GetTargetingCampaignGrowth2(GameEntity e, GameContext gameContext)
        {
            return GetBrandBasedAudienceGrowth(e, gameContext) * 3;
        }
        public static long GetTargetingCampaignGrowth(GameEntity e, GameContext gameContext)
        {
            return GetBrandBasedAudienceGrowth(e, gameContext);
        }

        public static Bonus<long> GetAudienceGrowthBonus(GameEntity product, GameContext gameContext)
        {
            var bonus = new Bonus<long>("Audience Growth");

            if (product.isRelease)
            {
                // Targeting
                if (Products.IsUpgradeEnabled(product, ProductUpgrade.TargetingCampaign))
                    bonus.AppendAndHideIfZero("Targeting Campaign", GetTargetingCampaignGrowth(product, gameContext));

                if (Products.IsUpgradeEnabled(product, ProductUpgrade.TargetingCampaign2))
                    bonus.AppendAndHideIfZero("Targeting Campaign II", GetTargetingCampaignGrowth2(product, gameContext));

                if (Products.IsUpgradeEnabled(product, ProductUpgrade.TargetingCampaign3))
                    bonus.AppendAndHideIfZero("Targeting Campaign III", GetTargetingCampaignGrowth3(product, gameContext));
            }

            if (!product.isRelease && Products.IsUpgradeEnabled(product, ProductUpgrade.TestCampaign))
                bonus.AppendAndHideIfZero("Test Campaign", C.TEST_CAMPAIGN_CLIENT_GAIN);

            return bonus;
        }

        public static long GetAudienceGrowth(GameEntity product, GameContext gameContext)
        {
            return GetAudienceGrowthBonus(product, gameContext).Sum();
        }
    }
}
