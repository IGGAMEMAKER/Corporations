using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetAudienceGrowthBySegment(GameEntity product, GameContext gameContext, int segmentId)
        {
            var channels = Markets.GetAllMarketingChannels(gameContext);
            long result = 0;

            foreach (var channelId in product.companyMarketingActivities.Channels.Keys)
            {
                var channel = channels.First(c => c.marketingChannel.ChannelInfo.ID == channelId);

                var gain = GetChannelClientGain(product, channel, segmentId);
                result += gain;
            }

            return result;
        }

        public static Bonus<long> GetAudienceGrowthBonus(GameEntity product, GameContext gameContext)
        {
            var bonus = new Bonus<long>("Audience Growth");

            var channels = Markets.GetAllMarketingChannels(gameContext);

            foreach (var channelId in product.companyMarketingActivities.Channels.Keys)
            {
                var channel = channels.First(c => c.marketingChannel.ChannelInfo.ID == channelId);

                var gain = GetChannelClientGain(product, channel);

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

            var segments = GetAudienceInfos();
            for (var i = 0; i < segments.Count; i++)
            {
               churnUsers += GetChurnClients(product, i);
            }

            bonus.Append("Marketing", GetAudienceGrowth(product, gameContext));
            bonus.Append("Negative loyalty", -churnUsers);
            bonus.MinifyValues();

            return bonus;
        }

        public static long GetAudienceChange(GameEntity product, GameContext gameContext)
        {
            return GetAudienceChange(product, gameContext, true).Sum();
        }

        public static bool IsImportantAudience(GameEntity product, int segmentId) => IsTargetAudience(product, segmentId);
        public static bool IsTargetAudience(GameEntity product, int segmentId)
        {
            return GetPositioning(product).Loyalties[segmentId] > 0;
        }

        public static int GetCoreAudienceId(GameEntity product)
        {
            if (product.isRelease)
            {
                // audience with most income?
                return 0;
            }

            var infos = Marketing.GetAudienceInfos();
            var maxAudience = infos.Count;

            return Mathf.Clamp(product.productPositioning.Positioning, 0, maxAudience - 1);
        }

        public static List<AudienceInfo> GetAudienceInfos()
        {
            var million = 1000000;

            var testAudience = new AudienceInfo
            {
                Name = "Test Audience",
                //"Messaging", "News Feed", "Friends list", "Voice chat", "Profile", "Video chat", "Sending files", "Emojis", "Likes", "Ads", "Ad panel"
                Needs = new List<int> { 5, 4, 3, 3, 2, 2, 2, 1, 1, },
                Icon = "Teenager",
                Size = 100,
                Bonuses = new List<FeatureBonus>
                {
                    new FeatureBonusMonetisation(-100),
                    new FeatureBonusRetention(5),
                    new FeatureBonusAcquisition(5)
                }
            };

            var list = new List<AudienceInfo>
            {
                //testAudience,
                new AudienceInfo {
                    Name = "Teenagers",
                    //"Messaging", "News Feed", "Friends list", "Voice chat", "Profile", "Video chat", "Sending files", "Emojis", "Likes"
                    //Needs = new List<int> { 5, 4, 3, 3, 2, 2, 2, 1, 1 },
                    Icon = "Teenager",
                    Size = 400 * million,
                },
                new AudienceInfo {
                    Name = "Adults (20+ years)",
                    //"Messaging", "News Feed", "Friends list", "Voice chat", "Profile", "Video chat", "Sending files", "Emojis", "Likes"
                    //Needs = new List<int> { 5, 2, 4, 3, 3, 1, 2, 1, 0, },
                    //Needs = "Needs messaging, profiles, friends, voice chats",
                    Icon = "Adult",
                    Size = 700 * million,
                },
                new AudienceInfo {
                    Name = "Middle aged people (35+)",
                    //Needs = "Needs messaging, profiles, friends, voice chats",
                    Icon = "Middle",
                    Size = 2000 * million,
                },
                new AudienceInfo {
                    Name = "Old people (50+)",
                    //Needs = "Needs messaging, friends, voice chats, video chats",
                    Icon = "Old",
                    Size = 100 * million,
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
