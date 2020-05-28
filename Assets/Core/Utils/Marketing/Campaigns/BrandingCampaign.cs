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

        public static bool IsCompanyActiveInChannel(GameEntity product, GameEntity channel)
        {
            return channel.channelMarketingActivities.Companies.ContainsKey(product.company.Id);
        }

        public static void ToggleChannelActivity(GameEntity product, GameContext gameContext, GameEntity channel)
        {
            var companyId = product.company.Id;

            var active = IsCompanyActiveInChannel(product, channel);

            if (active)
            {
                product.companyMarketingActivities.Channels.Remove(companyId);
                channel.channelMarketingActivities.Companies.Remove(companyId);
            }
            else
            {
                product.companyMarketingActivities.Channels[companyId] = 1;
                channel.channelMarketingActivities.Companies[companyId] = 1;
            }
        }
    }
}
