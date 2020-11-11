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

    bool ShowAffordableOnly = true;

    public Text MarketingEfficiency;

    public RenderAudiencesListView RenderAudiencesListView;
    int segmentId;

    public GameObject PossibleChannels;
    public GameObject AllChannels;

    public override void SetItem<T>(Transform t, T entity)
    {
        var channel = (GameEntity)(object)entity;

        t.GetComponent<MarketingChannelView>().SetEntity(channel, minROI, maxROI);
    }

    public void ToggleActiveChannels()
    {
        ShowAffordableOnly = !ShowAffordableOnly;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;
        
        // modal
        RenderMarketingEfficiencyInModal(company);

        CalculateROI(company);
        
        if (PossibleChannels != null)
            Draw(PossibleChannels, !ShowAffordableOnly);

        if (AllChannels != null)
            Draw(AllChannels, ShowAffordableOnly);
        
        // list
        var channels = ShowAffordableOnly ? Markets.GetAffordableMarketingChannels(company, Q) : Markets.GetTheoreticallyPossibleMarketingChannels(company, Q); // segmentId

        SetItems(channels.OrderByDescending(c => Marketing.GetChannelCost(company, c)));
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

        ShowAffordableOnly = true;

        ViewRender();
    }

    public void SetSegmentId(int id)
    {
        segmentId = id;

        ViewRender();
    }
}
