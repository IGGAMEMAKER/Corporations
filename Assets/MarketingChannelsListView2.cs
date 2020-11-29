using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketingChannelsListView2 : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<MarketingChannelView>().SetEntity(entity as GameEntity, 0, 100);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var channels = Markets.GetAffordableMarketingChannels(Flagship, Q).Take(3);

        SetItems(channels);
    }

    private void OnEnable()
    {
        ViewRender();
    }
}
