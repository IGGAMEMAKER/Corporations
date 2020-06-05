using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketingChannelsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var channel = (GameEntity)(object)entity;

        t.GetComponent<MarketingChannelView>().SetEntity(channel, channel == null);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var channels = new List<GameEntity>();

        var availableChannels = Markets.GetAvailableMarketingChannels(Q, Flagship)
            .OrderByDescending(c => c.marketingChannel.ChannelInfo.Audience);

        // hack to add Explore Channel Button
        var allMarketsCount = Markets.GetMarketingChannels(Q).Count();
        var exploredMarketsCount = Markets.GetAmountOfAvailableChannels(Q, Flagship);

        var isEploredAllMarkets = exploredMarketsCount >= allMarketsCount;

        if (!isEploredAllMarkets)
        {
            channels.Add(null);
        }
        //



        channels.AddRange(availableChannels);

        SetItems(channels);
    }
}
