using System.Linq;
using UnityEngine;

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

        public static int GetActiveChannelsLimit(GameEntity product)
        {
            return product.team.Teams.Select(Teams.GetTeamMarketingSlots).Sum();
        }

        public static int GetActiveChannelsCount(GameEntity product)
        {
            return product.companyMarketingActivities.Channels.Keys.Count;
        }

        public static bool IsNeedsMoreMarketersForCampaign(GameEntity product)
        {
            return GetActiveChannelsCount(product) >= GetActiveChannelsLimit(product);
        }

        public static bool IsActiveInChannel(GameEntity product, int channelId)
        {
            return product.companyMarketingActivities.Channels.ContainsKey(channelId);
            
            // // GameEntity channel
            // return channel.channelMarketingActivities.Companies.ContainsKey(product.company.Id);
        }

        public static int GetCampaignDuration(GameEntity product, long gain)
        {
            var duration = C.PERIOD * Mathf.Log(gain, 5) / 1.5f;

            return (int)duration;
        }
        public static int GetCampaignDuration(GameEntity product, ChannelInfo channelInfo)
        {
            var gain = Marketing.GetChannelClientGain(product, channelInfo);

            return GetCampaignDuration(product, gain);
        }

        public static Bonus<long> GetMarketingEfficiency(GameEntity company)
        {
            var b = new Bonus<long>("Gain");

            b.SetDimension("%");
            b.Append("From teams", Teams.GetMarketingEfficiency(company));

            foreach (var f in Products.GetAllFeaturesForProduct())
            {
                if (f.IsMonetizationFeature)
                    continue;

                if (Products.IsLeadingInFeature(company, f, null))
                {
                    b.Append("Leading in " + f.Name, 40);
                    continue;
                }

                if (Products.IsUpgradedFeature(company, f.Name))
                {
                    b.Append(f.Name, 10);
                }
            }

            return b;
        }

        public static long GetChannelClientGain(GameEntity company, int channelId) => GetChannelClientGain(company, company.channelInfos.ChannelInfos[channelId]);
        public static long GetChannelClientGain(GameEntity company, ChannelInfo channelInfo)
        {
            var batch = channelInfo.Batch;

            var bonus = GetMarketingEfficiency(company);

            return batch * bonus.Sum() / 100;
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
