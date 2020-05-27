using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketingChannelsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<MarketingChannelView>().SetEntity((ChannelInfo)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var channels = new List<ChannelInfo>();

        for (var i = 0; i < 6; i++)
        {
            var size = Random.Range(100, 500000);
            var batch = size / Random.Range(10, 100);

            channels.Add(new ChannelInfo { Audience = size, Companies = new Dictionary<int, long>(), costPerUser = 1.02f + Random.Range(-0.5f, 2f), ID = i, Batch = batch });
        }

        SetItems(channels);
    }
}
