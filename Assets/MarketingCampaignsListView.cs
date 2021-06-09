using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketingCampaignsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var view = t.GetComponent<MarketingCampaignView>();

        view.SetEntity(entity as ChannelInfo);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        //var channels = Markets.GetAffordableMarketingChannels(Flagship, Q);
        var channels = Markets.GetAllMarketingChannels(Q).Select(c => c.marketingChannel.ChannelInfo);

        SetItems(channels);
    }
}
