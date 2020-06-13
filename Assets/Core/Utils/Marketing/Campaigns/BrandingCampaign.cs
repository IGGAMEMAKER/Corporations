using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
    {
        // TODO remove
        public static long GetBrandingCost(GameEntity product, GameContext gameContext)
        {
            var clientCost = Markets.GetClientAcquisitionCost(product.product.Niche, gameContext);
            var flow = GetClientFlow(gameContext, product.product.Niche);

            var cost = clientCost * flow * 5;

            var discount = GetCorpCultureMarketingDiscount(product, gameContext);

            var result = cost * discount / 100;

            return (long)result;
        }

        public static float GetMarketingActivityCostPerUser(GameEntity product, GameContext gameContext, GameEntity channel)
        {
            return channel.marketingChannel.ChannelInfo.costPerUser;
        }
        public static long GetMarketingActivityCost(GameEntity product, GameContext gameContext, GameEntity channel)
        {
            //var clientCost = Markets.GetClientAcquisitionCost(product.product.Niche, gameContext);
            var clientCost = GetMarketingActivityCostPerUser(product, gameContext, channel);
            var flow = channel.marketingChannel.ChannelInfo.Batch;

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

            var marketingTeams = GetTeamsChannelReach(product, TeamType.MarketingTeam);
            var crossfunctionalTeams = GetTeamsChannelReach(product, TeamType.CrossfunctionalTeam);
            var smallUniversalTeams = GetTeamsChannelReach(product, TeamType.SmallCrossfunctionalTeam);
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



        public static bool ToggleChannelActivity(GameEntity product, GameContext gameContext, GameEntity channel)
        {
            var companyId = product.company.Id;
            var channelId = channel.marketingChannel.ChannelInfo.ID;

            var activeChannels = Marketing.GetAmountOfEnabledChannels(product);
            var maxChannels = Marketing.GetAmountOfChannelsThatYourTeamCanReach(product);

            bool hasEnoughWorkers = maxChannels > activeChannels;

            var active = IsCompanyActiveInChannel(product, channel);

            if (active)
            {
                DisableChannelActivity(product, gameContext, channel);
            }
            else
            {
                if (hasEnoughWorkers)
                    EnableChannelActivity(product, gameContext, channel);
                else
                    return false;
            }

            return true;
        }
    }
}
