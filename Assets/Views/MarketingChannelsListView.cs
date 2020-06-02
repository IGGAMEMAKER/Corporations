using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketingChannelsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<MarketingChannelView>().SetEntity((GameEntity)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        try
        {
            var channels = Markets.GetAvailableMarketingChannels(Q, Flagship)
                .OrderBy(c => c.marketingChannel.ChannelInfo.Audience);

            Debug.Log("Got channels");

            SetItems(channels);
        }
        catch
        {
            Debug.Log("Error: ");
            Debug.Log("Error: " + Flagship.company.Name);
        }

    }
}
