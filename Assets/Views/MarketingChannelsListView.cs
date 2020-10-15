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

        var company = Flagship;
        
        // modal
        RenderMarketingEfficiencyInModal(company);

        CalculateROI(company);
        

        // list
        var channels = Markets.GetMarketingChannels(company, Q).OrderByDescending(c => Marketing.GetChannelCost(company, c)); // segmentId

        SetItems(channels);
    }

    void CalculateROI(GameEntity company)
    {
        // calculate ROI
        var allChannels = Markets.GetAllMarketingChannels(Q);

        maxROI = allChannels.Max(c => Marketing.GetChannelCostPerUser(company, Q, c));
        minROI = allChannels.Min(c => Marketing.GetChannelCostPerUser(company, Q, c));
    }

    void RenderMarketingEfficiencyInModal(GameEntity company)
    {
        if (MarketingEfficiency != null)
        {
            var efficiency = Teams.GetMarketingEfficiency(company);

            MarketingEfficiency.text = efficiency + "%";
            MarketingEfficiency.color = Visuals.GetGradientColor(0, 100, efficiency);
        }
    }

    private void OnEnable()
    {
        segmentId = Marketing.GetCoreAudienceId(Flagship);
    }

    public void SetSegmentId(int id)
    {
        segmentId = id;

        ViewRender();
    }
}
