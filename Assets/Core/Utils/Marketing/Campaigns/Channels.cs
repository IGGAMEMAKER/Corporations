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
            return GetMarketingActivityCost(product, channel) / GetChannelClientGain(product, gameContext, channel);
        }

        public static bool IsCompanyActiveInChannel(GameEntity product, GameEntity channel)
        {
            return channel.channelMarketingActivities.Companies.ContainsKey(product.company.Id);
        }

        public static long GetChannelClientGain(GameEntity company, GameContext gameContext, GameEntity channel) => GetAudienceInfos().Select(i => GetChannelClientGain(company, gameContext, channel, i.ID)).Sum();
        public static long GetChannelClientGain(GameEntity company, GameContext gameContext, GameEntity channel, int segmentId)
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

            int teamId = GetTeamIdOfMarketingTask(company, channel);
            TeamInfo teamInfo = null;

            if (teamId >= 0)
                teamInfo = company.team.Teams[teamId];

            var marketingEffeciency = GetMarketingTeamEffeciency(gameContext, company, teamInfo);

            return batch * marketingEffeciency / 100;
        }

        public static int GetTeamIdOfMarketingTask(GameEntity company, GameEntity channel)
        {
            int counter = 0;

            var channelId = channel.marketingChannel.ChannelInfo.ID;

            foreach (var t in company.team.Teams)
            {
                if (t.Tasks.Find(tt => tt.IsMarketingTask && (tt as TeamTaskChannelActivity).ChannelId == channelId) != null)
                {
                    return counter;
                }

                counter++;
            }

            return -1;
        }


        public static int GetMarketingTeamEffeciency(GameContext gameContext, GameEntity company, TeamInfo teamInfo)
        {
            var marketingEffeciency = 0;

            if (teamInfo != null)
            {
                marketingEffeciency = Teams.GetEffectiveManagerRating(gameContext, company, WorkerRole.MarketingLead, teamInfo);

                marketingEffeciency *= teamInfo.TeamType == TeamType.MarketingTeam ? 2 : 1;

                marketingEffeciency += teamInfo.TeamType == TeamType.MarketingTeam ? 25 : 0;
            }

            return 50 + marketingEffeciency;
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
