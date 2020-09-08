using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetMarketingActivityCost(GameEntity product, GameContext gameContext, int ChannelId)
        {
            var channel = Markets.GetMarketingChannel(gameContext, ChannelId);

            return GetMarketingActivityCost(product, gameContext, channel);
        }
        public static long GetMarketingActivityCost(GameEntity product, GameContext gameContext, GameEntity channel)
        {
            return (long)channel.marketingChannel.ChannelInfo.costPerAd;
        }

        public static float GetChannelCostPerUser(GameEntity product, GameContext gameContext, GameEntity channel)
        {
            return GetMarketingActivityCost(product, gameContext, channel) / GetChannelClientGain(product, gameContext, channel);
        }

        public static bool IsCompanyActiveInChannel(GameEntity product, GameEntity channel)
        {
            return channel.channelMarketingActivities.Companies.ContainsKey(product.company.Id);
        }

        public static bool IsChannelExplored(GameEntity channel, GameEntity product)
        {
            return true;
            return product.channelExploration.Explored.Contains(channel.marketingChannel.ChannelInfo.ID);
        }

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

            int teamId = -1;
            int counter = 0;

            var channelId = channel.marketingChannel.ChannelInfo.ID;

            TeamInfo teamInfo = null;
            foreach (var t in company.team.Teams)
            {
                if (t.Tasks.Find(tt => tt.IsMarketingTask && (tt as TeamTaskChannelActivity).ChannelId == channelId) != null) {
                    teamId = counter;
                    teamInfo = t;

                    break;
                }

                counter++;
            }

            var marketingEffeciency = GetMarketingTeamEffeciency(gameContext, company, teamInfo);

            return batch * marketingEffeciency / 100;
        }

        public static int GetMarketingTeamEffeciency(GameContext gameContext, GameEntity company, TeamInfo teamInfo)
        {
            var marketingEffeciency = 0;

            if (teamInfo != null)
            {
                marketingEffeciency = Teams.GetEffectiveManagerRating(gameContext, company, WorkerRole.MarketingLead, teamInfo);

                marketingEffeciency *= teamInfo.TeamType == TeamType.MarketingTeam ? 2 : 1;
            }

            return 50 + marketingEffeciency;
        }

        public static long GetChannelClientGain(GameEntity company, GameContext gameContext, GameEntity channel)
        {
            var infos = GetAudienceInfos();

            return infos.Select(i => GetChannelClientGain(company, gameContext, channel, i.ID)).Sum();
        }

        // in months
        public static bool IsChannelProfitable(GameEntity company, GameContext gameContext, GameEntity channel, int segmentId) => true; // GetChannelRepaymentSpeed(company, gameContext, channel, segmentId) < 10000;
        public static float GetChannelROI(GameEntity company, GameContext gameContext, GameEntity channel, int segmentId)
        {
            var adCost = (float)GetMarketingActivityCost(company, gameContext, channel);

            var churn = GetChurnRate(gameContext, company, segmentId);

            var gainedUsers = GetChannelClientGain(company, gameContext, channel);

            if (adCost == 0)
            {
                return 1000000f;
            }
            return gainedUsers / adCost;

            var q = (100 - churn) / 100f;
            var b1 = gainedUsers * Economy.GetIncomePerUser(gameContext, company, segmentId);

            var Sum = b1 / (1 - q);

            if (Sum > adCost)
            {
                // channel is repaying 4 itself
                var Sn = adCost;
                var constr = 1 - (Sn * (1 - q) / b1);

                var n = Mathf.Log(constr, q);

                return n * C.PERIOD / 30;
            }

            // channel will never repay

            return 10000;
        }

        public static void ExploreChannel(GameEntity channel, GameEntity product)
        {
            var channelId = channel.marketingChannel.ChannelInfo.ID;

            if (!product.channelExploration.InProgress.ContainsKey(channelId))
                product.channelExploration.InProgress[channelId] = 7;
        }

        public static int GetAmountOfEnabledChannels(GameEntity product)
        {
            return product.companyMarketingActivities.Channels.Count;
        }

        static int GetTeamsChannelReach(GameEntity product, TeamType teamType)
        {
            return Teams.GetAmountOfTeams(product, teamType) * Teams.GetAmountOfPossibleChannelsByTeamType(teamType);
        }

        public static int GetAmountOfChannelsThatYourTeamCanReach(GameEntity product)
        {
            var teams = product.team.Teams;

            var marketingTeams          = GetTeamsChannelReach(product, TeamType.MarketingTeam);
            var crossfunctionalTeams    = GetTeamsChannelReach(product, TeamType.CrossfunctionalTeam);
            var smallUniversalTeams     = GetTeamsChannelReach(product, TeamType.SmallCrossfunctionalTeam);
            var bigCrossfunctionalTeams = GetTeamsChannelReach(product, TeamType.BigCrossfunctionalTeam);

            return marketingTeams + smallUniversalTeams + crossfunctionalTeams + bigCrossfunctionalTeams;
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
