using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetBrandingCost(GameEntity product, GameContext gameContext)
        {
            var clientCost = Markets.GetClientAcquisitionCost(product.product.Niche, gameContext);
            var flow = GetClientFlow(gameContext, product.product.Niche);

            var cost = clientCost * flow * 5;

            var discount = GetCorpCultureMarketingDiscount(product, gameContext);

            var result = cost * discount / 100;

            return (long)result;
        }

        public static long GetMarketingActivityCost(GameEntity product, GameContext gameContext, GameEntity channel)
        {
            var clientCost = Markets.GetClientAcquisitionCost(product.product.Niche, gameContext);
            var flow = channel.marketingChannel.ChannelInfo.Batch; // GetClientFlow(gameContext, product.product.Niche);

            var cost = clientCost * flow;

            return (long)cost;
        }

        public static bool IsCompanyActiveInChannel(GameEntity product, GameEntity channel)
        {
            return channel.channelMarketingActivities.Companies.ContainsKey(product.company.Id);
        }

        public static bool IsChannelExplored(GameEntity channel, GameEntity product)
        {
            return product.channelExploration.Explored.Contains(channel.marketingChannel.ChannelInfo.ID);
        }

        public static void ExploreChannel(GameEntity channel, GameEntity product)
        {
            var channelId = channel.marketingChannel.ChannelInfo.ID;

            if (!product.channelExploration.InProgress.ContainsKey(channelId))
                product.channelExploration.InProgress[channelId] = 7;
        }

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



        public static void ToggleChannelActivity(GameEntity product, GameContext gameContext, GameEntity channel)
        {
            var companyId = product.company.Id;
            var channelId = channel.marketingChannel.ChannelInfo.ID;

            var active = IsCompanyActiveInChannel(product, channel);

            if (active)
            {
                DisableChannelActivity(product, gameContext, channel);
            }
            else
            {
                EnableChannelActivity(product, gameContext, channel);
            }
        }
    }
}
