using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketingChannelsListView : ListView
{
    float maxROI = 0;
    float minROI = 0;
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var channel = (GameEntity)(object)entity;

        t.GetComponent<MarketingChannelView>().SetEntity(channel, minROI, maxROI, channel == null);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var channels = new List<GameEntity>();

        var company = Flagship;

        var availableChannels = Markets.GetAvailableMarketingChannels(Q, company)
            .OrderByDescending(c => c.marketingChannel.ChannelInfo.Audience);

        //
        channels.AddRange(availableChannels);

        maxROI = channels.Max(c => Marketing.GetChannelROI(company, Q, c));
        minROI = channels.Min(c => Marketing.GetChannelROI(company, Q, c));

        // hack to add Explore Channel Button
        var allMarketsCount = Markets.GetMarketingChannels(Q).Count();
        var exploredMarketsCount = Markets.GetAmountOfAvailableChannels(Q, company);

        var isEploredAllMarkets = exploredMarketsCount >= allMarketsCount;

        if (!isEploredAllMarkets)
        {
            channels.Insert(0, null);
        }

        SetItems(channels);
    }
}
