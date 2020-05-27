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

        var channels = Markets.GetMarketingChannels(Q);

        SetItems(channels);
    }
}
