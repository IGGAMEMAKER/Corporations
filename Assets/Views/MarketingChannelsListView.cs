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

        //var segmentId = company.productTargetAudience.SegmentId;

        var availableChannels = Markets.GetAvailableMarketingChannels(Q, company, ShowActiveChannelsToo)
            .Where(c => Marketing.IsChannelProfitable(company, Q, c, segmentId));

        //channels.AddRange(availableChannels.OrderByDescending(c => c.marketingChannel.ChannelInfo.Audience));
        channels.AddRange(availableChannels.OrderByDescending(c => Marketing.GetChannelClientGain(company, Q, c, segmentId)));


        var allChannels = Markets.GetMarketingChannels(Q);
        ////maxROI = channels.Max(c => Marketing.GetChannelROI(company, Q, c));
        //maxROI = allChannels.Max(c => Marketing.GetChannelROI(company, Q, c));
        //minROI = allChannels.Min(c => Marketing.GetChannelROI(company, Q, c));

        maxROI = allChannels.Max(c => Marketing.GetChannelCostPerUser(company, Q, c));
        minROI = allChannels.Min(c => Marketing.GetChannelCostPerUser(company, Q, c));

        SetItems(channels);
    }

    private void OnEnable()
    {
        segmentId = Flagship.productTargetAudience.SegmentId;
    }

    public void SetSegmentId(int id)
    {
        segmentId = id;

        ViewRender();
    }
}
