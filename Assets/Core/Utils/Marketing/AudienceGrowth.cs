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

            foreach (var channelId in product.companyMarketingActivities.Channels.Keys)
            {
                var channel = channels.First(c => c.marketingChannel.ChannelInfo.ID == channelId);

                var gain = GetChannelClientGain(product, gameContext, channel);
                bonus.AppendAndHideIfZero("Channel " + channelId, gain);
            }

            return bonus;
        }

        public static long GetAudienceGrowth(GameEntity product, GameContext gameContext)
        {
            return GetAudienceGrowthBonus(product, gameContext).Sum();
        }

        public static Bonus<long> GetAudienceChange(GameEntity product, GameContext gameContext, bool isBonus)
        {
            var bonus = new Bonus<long>("Audience change");

            long churnUsers = 0;

            var segments = Marketing.GetAudienceInfos();
            for (var i = 0; i < segments.Count; i++)
            {
               churnUsers += Marketing.GetChurnClients(gameContext, product.company.Id, i);
            }

            bonus.Append("Audience Growth", Marketing.GetAudienceGrowth(product, gameContext));
            bonus.Append("Audience Loss (Churn)", -churnUsers);
            bonus.MinifyValues();

            return bonus;
        }

        public static long GetAudienceChange(GameEntity product, GameContext gameContext)
        {
            return GetAudienceChange(product, gameContext, true).Sum();
        }



        public static List<AudienceInfo> GetAudienceInfos()
        {
            var million = 1000000;

            var testAudience = new AudienceInfo
            {
                Name = "Test Audience",
                Needs = "Needs messaging, profiles, friends",
                Icon = "Teenager",
                Amount = 100,
                Bonuses = new List<FeatureBonus>
                {
                    new FeatureBonusMonetisation(-100),
                    new FeatureBonusRetention(5),
                    new FeatureBonusAcquisition(5)
                }
            };

            var list = new List<AudienceInfo>
            {
                testAudience,
                new AudienceInfo {
                    Name = "Teenagers",
                    Needs = "Needs messaging, profiles, friends, voice chats, video chats, emojis, file sending",
                    Icon = "Teenager",
                    Amount = 400 * million,
                },
                new AudienceInfo {
                    Name = "Adults (20-30 years)",
                    Needs = "Needs messaging, profiles, friends, voice chats",
                    Icon = "Adult",
                    Amount = 700 * million,
                },
                new AudienceInfo {
                    Name = "Middle aged people (30+)",
                    Needs = "Needs messaging, profiles, friends, voice chats",
                    Icon = "Middle",
                    Amount = 2000 * million,
                },
                new AudienceInfo {
                    Name = "Old people",
                    Needs = "Needs messaging, friends, voice chats, video chats",
                    Icon = "Old",
                    Amount = 100 * million,
                },
            };

            return WrapIndices(list);
        }

        static List<FeatureBonus> GetRandomFeatureBonuses(int segmentId)
        {
            return new List<FeatureBonus>
            {
                new FeatureBonusMonetisation(Companies.GetRandomValueInRange(-15, 15f, segmentId, 1)),
                new FeatureBonusAcquisition(Companies.GetRandomValueInRange(-5, 25f, segmentId, 2)),
                new FeatureBonusRetention(Companies.GetRandomValueInRange(-10, 5f, segmentId, 3))
                //new FeatureBonusRetention(Random.Range(-10, 5f))
            };
        }

        static List<AudienceInfo> WrapIndices(List<AudienceInfo> audienceInfos)
        {
            for (var i = 0; i < audienceInfos.Count; i++)
            {
                audienceInfos[i].ID = i;
                audienceInfos[i].Bonuses = GetRandomFeatureBonuses(i);
            }

            return audienceInfos;
        }
    }
}
