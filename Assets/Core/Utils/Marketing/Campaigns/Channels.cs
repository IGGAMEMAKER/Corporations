using System.Linq;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetChannelCost(GameEntity product, int channelId)
        {
            return (long)product.channelInfos.ChannelInfos[channelId].costPerAd;
            // return (long)channel.marketingChannel.ChannelInfo.costPerAd;
        }

        public static float GetChannelCostPerUser(GameEntity product, int channelId)
        {
            return GetChannelCost(product, channelId) * 1f / GetChannelClientGain(product, channelId);
        }

        public static bool IsActiveInChannel(GameEntity product, int channelId)
        {
            return product.companyMarketingActivities.Channels.ContainsKey(channelId);
            
            // // GameEntity channel
            // return channel.channelMarketingActivities.Companies.ContainsKey(product.company.Id);
        }

        // fraction will be recalculated
        // take into account
        // * Base channel width (f.e. 100K users per week)

        // * proportions (teens: 90%, olds: 10%)
        // * random anomalies (there are more people of specific segment (especially in small channels)) teens: 80%, olds: 20%)
        // * Base user activity (desire to click on ads: 5% => we can get 5K users)
        // * segment bonuses (audience may be small, but it is way more active (desire to click X2) and you can get more)
        // * positioning bonuses
        public static long GetChannelClientGain(GameEntity company, int channelId) =>
            GetAudienceInfos().Select(i => GetChannelClientGain(company, company.channelInfos.ChannelInfos[channelId], i.ID)).Sum();

        public static long GetChannelClientGain(GameEntity company, int channelId, int segmentId) =>
            GetChannelClientGain(company, company.channelInfos.ChannelInfos[channelId], segmentId);
        
        public static long GetChannelClientGain(GameEntity company, ChannelInfo channelInfo, int segmentId)
        {
            var baseChannelBatch = channelInfo.Batch;
            var batch = baseChannelBatch;

            var marketingEfficiency = Teams.GetMarketingEfficiency(company);
            // From feature count

            return batch * (marketingEfficiency) / 100;
        }

        // in months
        public static void EnableChannelActivity(GameEntity product, GameEntity channel)
        {
            var companyId = product.company.Id;
            var channelId = channel.marketingChannel.ChannelInfo.ID;

            product.companyMarketingActivities.Channels[channelId] = 1;
            channel.channelMarketingActivities.Companies[companyId] = 1;
        }

        public static void DisableChannelActivity(GameEntity product, GameEntity channel)
        {
            var companyId = product.company.Id;
            var channelId = channel.marketingChannel.ChannelInfo.ID;

            product.companyMarketingActivities.Channels.Remove(channelId);
            channel.channelMarketingActivities.Companies.Remove(companyId);
        }
    }
}
