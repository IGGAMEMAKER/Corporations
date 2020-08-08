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

        public static bool IsCompanyActiveInChannel(GameEntity product, GameEntity channel)
        {
            return channel.channelMarketingActivities.Companies.ContainsKey(product.company.Id);
        }

        public static bool IsChannelExplored(GameEntity channel, GameEntity product)
        {
            return true;
            return product.channelExploration.Explored.Contains(channel.marketingChannel.ChannelInfo.ID);
        }

        public static long GetChannelClientGain(GameEntity company, GameContext gameContext, GameEntity channel)
        {
            var fraction = (double)Companies.GetHashedRandom2(company.company.Id, channel.marketingChannel.ChannelInfo.ID + company.productTargetAudience.SegmentId);
            var batch = (long)(channel.marketingChannel.ChannelInfo.Batch * fraction);

            var marketingEffeciency = Teams.GetEffectiveManagerRating(gameContext, company, WorkerRole.MarketingLead);
            var acquisitionEffeciency = Products.GetAcquisitionFeaturesBenefit(company);

            var gainedAudience = batch * (100 + marketingEffeciency + (int)acquisitionEffeciency) / 100;


            return gainedAudience;
        }

        // in months
        public static bool IsChannelProfitable(GameEntity company, GameContext gameContext, GameEntity channel, int segmentId) => GetChannelRepaymentSpeed(company, gameContext, channel, segmentId) < 10000;
        public static float GetChannelRepaymentSpeed(GameEntity company, GameContext gameContext, GameEntity channel, int segmentId)
        {
            var adCost = (float)GetMarketingActivityCost(company, gameContext, channel);
            var churn = GetChurnRate(gameContext, company, segmentId);

            var gainedUsers = GetChannelClientGain(company, gameContext, channel);

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

        public static float GetChannelROI(GameEntity company, GameContext gameContext, GameEntity channel, int segmentId)
        {
            var lifetime = Marketing.GetLifeTime(gameContext, company.company.Id);
            //var lifetimeFormatted = lifetime.ToString("0.00");

            var incomePerUser = Economy.GetIncomePerUser(gameContext, company, segmentId);
            
            // this formula doesn't include team maintenance cost
            var cost = Marketing.GetMarketingActivityCost(company, gameContext, channel);

            //// so i need to add it
            //cost += (long) channel.marketingChannel.ChannelInfo.costInWorkers * C.SALARIES_PROGRAMMER * 3;

            var userGain = GetChannelClientGain(company, gameContext, channel);

            var ROI = incomePerUser * userGain * lifetime * (100 - 1) / cost;

            return ROI;
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
