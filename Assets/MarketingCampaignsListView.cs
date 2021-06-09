using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketingCampaignsListView : ListView
{
    public Transform animationTransform;

    public override void SetItem<T>(Transform t, T entity)
    {
        var view = t.GetComponent<MarketingCampaignView>();

        view.SetEntity(entity as ChannelInfo, animationTransform);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var channels = Markets.GetMaintainableMarketingChannels(Flagship, Q);

        SetItems(
            channels
            .OrderByDescending(c => c.Batch)
            );
    }
}
