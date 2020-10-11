using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarketingChannelsListView : ListView
{
    float maxROI = 0;
    float minROI = 0;

    bool ShowActiveChannelsToo = false;

    public Text MarketingEfficiency;

    public RenderAudiencesListView RenderAudiencesListView;
    int segmentId;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var channel = (GameEntity)(object)entity;

        t.GetComponent<MarketingChannelView>().SetEntity(channel, minROI, maxROI, RenderAudiencesListView);
    }

    public void ToggleActiveChannels()
    {
        ShowActiveChannelsToo = !ShowActiveChannelsToo;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var channels = new List<GameEntity>();

        var company = Flagship;


        var availableChannels = Markets.GetMarketingChannelsList(company, Q);

        channels.AddRange(availableChannels.OrderByDescending(c => Marketing.GetChannelClientGain(company, c))); // segmentId

        var allChannels = Markets.GetMarketingChannels(Q);

        maxROI = allChannels.Max(c => Marketing.GetChannelCostPerUser(company, Q, c));
        minROI = allChannels.Min(c => Marketing.GetChannelCostPerUser(company, Q, c));

        SetItems(channels);

        if (MarketingEfficiency != null)
            MarketingEfficiency.text = Visuals.Positive(Teams.GetMarketingEfficiency(Flagship) + "%");
    }

    private void OnEnable()
    {
        segmentId = Marketing.GetCoreAudienceId(Flagship, Q);
    }

    public void SetSegmentId(int id)
    {
        segmentId = id;

        ViewRender();
    }
}
