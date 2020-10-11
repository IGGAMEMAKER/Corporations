using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetMarketingActivityCost(GameEntity product, GameContext gameContext, int ChannelId) => GetMarketingActivityCost(product, Markets.GetMarketingChannel(gameContext, ChannelId));
        public static long GetMarketingActivityCost(GameEntity product, GameEntity channel)
        {
            return (long)channel.marketingChannel.ChannelInfo.costPerAd;
        }

        public static float GetChannelCostPerUser(GameEntity product, GameContext gameContext, GameEntity channel)
        {
            return GetMarketingActivityCost(product, channel) / GetChannelClientGain(product, channel);
        }

        public static bool IsCompanyActiveInChannel(GameEntity product, GameEntity channel)
        {
            return channel.channelMarketingActivities.Companies.ContainsKey(product.company.Id);
        }

        public static long GetChannelGainLoyaltyBonus(GameEntity company, GameEntity channel, int segmentId)
        {
            var loyalty = (int)GetSegmentLoyalty(company, segmentId);

            int loyaltyBonus = 0;
            if (loyalty >= 0)
            {
                loyaltyBonus = loyalty * 10;

                if (loyalty > 10)
                    loyaltyBonus = 10 * 10 + (loyalty - 10) * 5;

                if (loyalty > 20)
                    loyaltyBonus = 10 * 10 + 10 * 5 + (loyalty - 20) * 1;
            }

            return loyaltyBonus;
        }

        public static long GetChannelClientGain(GameEntity company, GameEntity channel) => GetAudienceInfos().Select(i => GetChannelClientGain(company, channel, i.ID)).Sum();
        public static long GetChannelClientGain(GameEntity company, GameEntity channel, int segmentId)
        {
            var fraction = (double)Companies.GetHashedRandom2(company.company.Id, channel.marketingChannel.ChannelInfo.ID + segmentId);

            // fraction will be recalculated
            // take into account
            // * Base channel width (f.e. 100K users per week)

            // * proportions (teens: 90%, olds: 10%)
            // * random anomalies (there are more people of specific segment (especially in small channels)) teens: 80%, olds: 20%)
            // * Base user activity (desire to click on ads: 5% => we can get 5K users)
            // * segment bonuses (audience may be small, but it is way more active (desire to click X2) and you can get more)
            // * positioning bonuses

            var batch = (long)(channel.marketingChannel.ChannelInfo.Batch * fraction);

            var positioning = GetPositioning(company);
            var positioningLoyalty = positioning.Loyalties[segmentId];

            // cannot get other segments if our product is not targeted for them
            if (positioningLoyalty < 0)
                return 0;

            var loyaltyBonus = GetChannelGainLoyaltyBonus(company, channel, segmentId);
            
            // cannot get clients if existing ones are outraged
            if (loyaltyBonus <= 0)
                return 0;

            var marketingEffeciency = Teams.GetMarketingEfficiency(company);

            return batch * (marketingEffeciency + loyaltyBonus) / 100;
        }

        // in months
        public static void EnableChannelActivity(GameEntity product, GameContext gameContext, GameEntity channel)
        {
            var companyId = product.company.Id;
            var channelId = channel.marketingChannel.ChannelInfo.ID;

            product.companyMarketingActivities.Channels[channelId] = 1;
            channel.channelMarketingActivities.Companies[companyId] = 1;
        }

        public static void DisableChannelActivity(GameEntity product, GameContext gameContext, GameEntity channel)
        {
            var companyId = product.company.Id;
            var channelId = channel.marketingChannel.ChannelInfo.ID;

            product.companyMarketingActivities.Channels.Remove(channelId);
            channel.channelMarketingActivities.Companies.Remove(companyId);
        }



        public static void ToggleChannelActivity(GameEntity product, GameContext gameContext, GameEntity channel, int teamId, int taskId)
        {
            var active = IsCompanyActiveInChannel(product, channel);

            if (active)
            {
                DisableChannelActivity(product, gameContext, channel);
            }
            else
            {
                //EnableChannelActivity(product, gameContext, channel);

                var channelId = channel.marketingChannel.ChannelInfo.ID;
                Teams.AddTeamTask(product, gameContext, teamId, taskId, new TeamTaskChannelActivity(channelId));
            }
        }
    }
}
