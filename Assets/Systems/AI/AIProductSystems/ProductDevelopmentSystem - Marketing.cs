using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    void ReleaseProduct(GameEntity product)
    {
        if (Companies.IsReleaseableApp(product))
            Marketing.ReleaseApp(gameContext, product);
    }

    IEnumerable<ChannelInfo> GetNotActiveChannels(GameEntity product)
    {
        // var channels = Markets.GetAllMarketingChannels(product);
        var channels = Markets.GetTheoreticallyPossibleMarketingChannels(product);

        return channels.OrderBy(c => c.costPerAd);
    }

    ChannelInfo GetBestChannel(GameEntity product)
    {
        return Markets.GetAffordableMarketingChannels(product, gameContext).FirstOrDefault();
        var spareBudget = Economy.GetSpareBudget(product, gameContext, 1);
        
        // return GetNotActiveChannels(product).FirstOrDefault();
        return GetNotActiveChannels(product).FirstOrDefault(c => (long) c.costPerAd < spareBudget);
        // return GetNotActiveChannels(product).FirstOrDefault(c => CanMaintain(product, (long) c.costPerAd));
    }
    
    void ManageChannels(GameEntity product)
    {
        // var bestChannel = GetBestChannel(product);
        var channels = Markets.GetAffordableMarketingChannels(product, gameContext)
        //     .OrderBy(c => Marketing.GetChannelCostPerUser(product, c.ID))
        //     .ThenByDescending(c => Marketing.GetChannelCost(product, c.ID))
        ;

        
        if (channels.Any())
        {
            var c = channels.First();

            TryAddTask(product, TeamTaskChannelActivity.FromChannel(c));
        }
    }
}
