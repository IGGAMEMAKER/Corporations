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

        view.SetEntity(entity as MarketingChannelComponent);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var channels = Markets.GetAllMarketingChannels(Q);

        SetItems(channels.Select(c => c.marketingChannel));
    }
}
