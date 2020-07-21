using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            var flow = GetClientFlow(gameContext, e.product.Niche);

            bool isMassPositioning = true;
            bool isNichePositioning = !isMassPositioning;

            if (isNichePositioning)
            {
                var brand = (int)e.branding.BrandPower;

                flow /= 10;

                return flow;
            }

            if (isMassPositioning)
            {
                var brandBasedMarketShare = GetBrandBasedMarketShare(e, gameContext) + 0.05f;

                return (long)(brandBasedMarketShare * flow);
            }

            return 0;
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

            var channels = Markets.GetMarketingChannels(gameContext);

            //if (product.isRelease)
            //{
            //    // Targeting
            //    if (Products.IsUpgradeEnabled(product, ProductUpgrade.TargetingCampaign))
            //        bonus.AppendAndHideIfZero("Targeting Campaign", GetTargetingCampaignGrowth(product, gameContext));

            //    if (Products.IsUpgradeEnabled(product, ProductUpgrade.TargetingCampaign2))
            //        bonus.AppendAndHideIfZero("Targeting Campaign II", GetTargetingCampaignGrowth2(product, gameContext));

            //    if (Products.IsUpgradeEnabled(product, ProductUpgrade.TargetingCampaign3))
            //        bonus.AppendAndHideIfZero("Targeting Campaign III", GetTargetingCampaignGrowth3(product, gameContext));
            //}

            //if (!product.isRelease && Products.IsUpgradeEnabled(product, ProductUpgrade.TestCampaign))
            //    bonus.AppendAndHideIfZero("Test Campaign", C.TEST_CAMPAIGN_CLIENT_GAIN);

            foreach (var channelId in product.companyMarketingActivities.Channels.Keys)
            {
                var channel = channels.First(c => c.marketingChannel.ChannelInfo.ID == channelId);

                var gain = GetChannelClientGain(product, gameContext, channel);
                bonus.AppendAndHideIfZero("Forum " + channelId, gain);
            }

            return bonus;
        }

        public static long GetAudienceGrowth(GameEntity product, GameContext gameContext)
        {
            return GetAudienceGrowthBonus(product, gameContext).Sum();
        }

        public static long GetAudienceChange(GameEntity product, GameContext gameContext)
        {
            var loss = Marketing.GetChurnClients(gameContext, product.company.Id);

            return Marketing.GetAudienceGrowth(product, gameContext) - loss;
        }


        public static List<AudienceInfo> GetAudienceInfos()
        {
            var million = 1000000;
            return new List<AudienceInfo>
            {
                new AudienceInfo { Loyalty = Random.Range(-15, 20), Name = "Teenagers",                 Needs = "Needs messaging, profiles, friends, voice chats, video chats, emojis, file sending", Icon = "Teenager", Amount = 400 * million },
                new AudienceInfo { Loyalty = Random.Range(-15, 20), Name = "Adults (20-30 years)",      Needs = "Needs messaging, profiles, friends, voice chats", Icon = "Adult", Amount = 700 * million },
                new AudienceInfo { Loyalty = Random.Range(-15, 20), Name = "Middle aged people (30+)",  Needs = "Needs messaging, profiles, friends, voice chats", Icon = "Middle", Amount = 2000 * million },
                new AudienceInfo { Loyalty = Random.Range(-15, 20), Name = "Old people",                Needs = "Needs messaging, friends, voice chats, video chats", Icon = "Old", Amount = 100 * million },
            };
        }
    }
}
