using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketingChannelsListView : ListView
{
    float maxROI = 0;
    float minROI = 0;

    bool ShowActiveChannelsToo = false;

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

        bool didMarketingCampaigns = Marketing.GetClients(company) > 50;
        bool didFeatures = company.features.Upgrades.Count > 0;

        int counter = 0;

        if (didFeatures)
        {
            counter = 1;

            if (didMarketingCampaigns)
                counter = 4;

            if (company.isRelease)
                counter = 8;
        }

        var availableChannels = Markets.GetAvailableMarketingChannels(Q, company, ShowActiveChannelsToo)
            .Where(c => didMarketingCampaigns || Marketing.GetChannelCostPerUser(company, Q, c) == 0)
            .TakeWhile(c => counter-- > 0)
            ;

        channels.AddRange(availableChannels.OrderByDescending(c => Marketing.GetChannelClientGain(company, Q, c))); // segmentId


        var allChannels = Markets.GetMarketingChannels(Q);

        maxROI = allChannels.Max(c => Marketing.GetChannelCostPerUser(company, Q, c));
        minROI = allChannels.Min(c => Marketing.GetChannelCostPerUser(company, Q, c));

        SetItems(channels);
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
