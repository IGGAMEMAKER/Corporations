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

        t.GetComponent<MarketingChannelView>().SetEntity(channel, minROI, maxROI);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var channels = new List<GameEntity>();

        var company = Flagship;

        var availableChannels = Markets.GetAvailableMarketingChannels(Q, company);

        var clients = Marketing.GetClients(company);



        var newChannels = availableChannels
            //.Where(c => !Marketing.IsCompanyActiveInChannel(company, c))
            .Where(c => c.marketingChannel.ChannelInfo.Batch < clients / 4);

        if (newChannels.Count() == 0)
        {
            // ensure, that we have at least one channel
            newChannels = availableChannels
                .OrderBy(c => c.marketingChannel.ChannelInfo.Audience)
                .Take(1);
        }



        channels.AddRange(newChannels.OrderByDescending(c => c.marketingChannel.ChannelInfo.Audience));

        maxROI = channels.Max(c => Marketing.GetChannelROI(company, Q, c));
        minROI = channels.Min(c => Marketing.GetChannelROI(company, Q, c));

        SetItems(channels);
    }
}
