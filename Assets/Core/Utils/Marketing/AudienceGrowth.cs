﻿using System.Linq;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static float GetSumOfBrandPowers(NicheType nicheType, GameContext gameContext) => GetSumOfBrandPowers(Markets.GetNiche(gameContext, nicheType), gameContext);
        public static float GetSumOfBrandPowers(GameEntity niche, GameContext gameContext)
        {
            var products = Markets.GetProductsOnMarket(niche, gameContext);

            var sumOfBrandPowers = products.Sum(p => p.branding.BrandPower);

            return sumOfBrandPowers;
        }
        public static float GetBrandBasedMarketShare(GameEntity e, GameContext gameContext)
        {
            var niche = Markets.GetNiche(gameContext, e);

            var sumOfBrandPowers = GetSumOfBrandPowers(niche, gameContext);

            // +1 : avoid division by zero
            return e.branding.BrandPower / (sumOfBrandPowers + 1);
        }

        public static long GetTargetingCampaignGrowth(GameEntity e, GameContext gameContext)
        {
            return GetBrandBasedAudienceGrowth(e, gameContext);
        }
        public static long GetBrandBasedAudienceGrowth(GameEntity e, GameContext gameContext)
        {
            var brandBasedMarketShare = GetBrandBasedMarketShare(e, gameContext);

            var flow = GetClientFlow(gameContext, e.product.Niche);

            return (long)(brandBasedMarketShare * flow);
        }

        public static int GetMarketingLeadBonus(GameEntity product, GameContext gameContext)
        {
            var marketingLead = Teams.GetWorkerByRole(product, WorkerRole.MarketingLead, gameContext);

            var marketingBonus = 100;

            if (marketingLead != null)
            {
                var rating = Humans.GetRating(gameContext, marketingLead);
                var effeciency = Teams.GetWorkerEffeciency(marketingLead, product);

                marketingBonus += 30 * rating * effeciency / 100 / 100;
            }

            return marketingBonus;
        }

        public static Bonus<long> GetAudienceGrowthBonus(GameEntity product, GameContext gameContext)
        {
            var bonus = new Bonus<long>("Audience Growth");

            if (product.isRelease && Products.IsUpgradeEnabled(product, ProductUpgrade.TargetingInSocialNetworks))
                bonus.AppendAndHideIfZero("Targeting Campaign", GetTargetingCampaignGrowth(product, gameContext));

            if (!product.isRelease && Products.IsUpgradeEnabled(product, ProductUpgrade.TestCampaign))
                bonus.AppendAndHideIfZero("Test Campaign", Balance.TEST_CAMPAIGN_CLIENT_GAIN);

            return bonus;
        }

        public static long GetAudienceGrowth(GameEntity product, GameContext gameContext)
        {
            return GetAudienceGrowthBonus(product, gameContext).Sum();
        }
    }
}