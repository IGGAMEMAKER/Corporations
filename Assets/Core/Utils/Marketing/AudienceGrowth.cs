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

            return product.companyMarketingActivities.Channels.Keys
                // .Select(channelId => channels.First(c => c.marketingChannel.ChannelInfo.ID == channelId))
                .Select(channel => GetChannelClientGain(product, channel, segmentId))
                .Sum();
        }

        public static Bonus<long> GetAudienceGrowthBonus(GameEntity product)
        {
            var bonus = new Bonus<long>("Audience Growth");

            foreach (var channelId in product.companyMarketingActivities.Channels.Keys)
            {
                var gain = GetChannelClientGain(product, channelId);

                bonus.AppendAndHideIfZero("Channel " + channelId, gain);
            }

            return bonus;
        }

        public static long GetAudienceGrowth(GameEntity product, GameContext gameContext)
        {
            return GetAudienceGrowthBonus(product).Sum();
        }

        public static Bonus<long> GetAudienceChange(GameEntity product, GameContext gameContext, bool isBonus)
        {
            var bonus = new Bonus<long>("Audience change");

            long churnUsers = 0;

            var segments = GetAudienceInfos();
            for (var i = 0; i < segments.Count; i++)
            {
               churnUsers += GetChurnClients(product, gameContext, i);
            }

            bonus.Append("Marketing", GetAudienceGrowth(product, gameContext));
            bonus.Append("Loss", -churnUsers);
            bonus.MinifyValues();

            return bonus;
        }

        public static long GetAudienceChange(GameEntity product, GameContext gameContext)
        {
            return GetAudienceChange(product, gameContext, true).Sum();
        }

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

            return GetBaseAudienceId(product);
        }

        public static int GetBaseAudienceId(GameEntity product)
        {
            var infos = Marketing.GetAudienceInfos();
            var maxAudience = infos.Count;

            return Mathf.Clamp(product.productPositioning.Positioning, 0, maxAudience - 1);
        }

        public static List<AudienceInfo> GetAudienceInfos()
        {
            var million = 1000000;

            var list = new List<AudienceInfo>
            {
                new AudienceInfo {
                    Name = "Teenagers",
                    Icon = "Teenager",
                    Size = 400 * million,
                },
                new AudienceInfo {
                    Name = "Adults (20+ years)",
                    Icon = "Adult",
                    Size = 700 * million,
                },
                new AudienceInfo {
                    Name = "Middle aged people (35+)",
                    Icon = "Middle",
                    Size = 2000 * million,
                },
                new AudienceInfo {
                    Name = "Old people (50+)",
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
                new FeatureBonusMonetization(Companies.GetRandomValueInRange(-15, 15f, segmentId, 1)),
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
