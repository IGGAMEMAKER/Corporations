using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketingChannelsListView : ListView
{
    float maxROI = 0;
    float minROI = 0;
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var channel = (GameEntity)(object)entity;

        t.GetComponent<MarketingChannelView>().SetEntity(channel, minROI, maxROI, channel == null);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var channels = new List<GameEntity>();

        var company = Flagship;

        var availableChannels = Markets.GetAvailableMarketingChannels(Q, company);

        var clients = Marketing.GetClients(company);

        var freeChannels = Marketing.GetAmountOfChannelsThatYourTeamCanReach(company) - Marketing.GetAmountOfEnabledChannels(company);
        if (freeChannels < 0)
            freeChannels = 0;

        var exploredChannels = availableChannels.Where(c => Marketing.IsChannelExplored(c, company));
        var newChannels = availableChannels
            .Where(c => !Marketing.IsChannelExplored(c, company) && c.marketingChannel.ChannelInfo.Batch < clients / 4)
            .Take(freeChannels);

        var chosenChannels = new List<GameEntity>();

        if (exploredChannels.Count() > 0)
            chosenChannels.AddRange(exploredChannels);

        if (newChannels.Count() == 0)
        {
            // ensure, that we have at least one channel
            newChannels = availableChannels
                .OrderBy(c => c.marketingChannel.ChannelInfo.Audience)
                .Where(c => !Marketing.IsChannelExplored(c, company))
                .Take(1);

        }
        chosenChannels.AddRange(newChannels);

        channels.AddRange(chosenChannels.OrderByDescending(c => c.marketingChannel.ChannelInfo.Audience));

        maxROI = channels.Max(c => Marketing.GetChannelROI(company, Q, c));
        minROI = channels.Min(c => Marketing.GetChannelROI(company, Q, c));

        //// hack to add Explore Channel Button
        //var allMarketsCount = Markets.GetMarketingChannels(Q).Count();
        //var exploredMarketsCount = Markets.GetAmountOfAvailableChannels(Q, company);

        //var isEploredAllMarkets = exploredMarketsCount >= allMarketsCount;

        //if (!isEploredAllMarkets)
        //{
        //    channels.Insert(0, null);
        //}

        SetItems(channels);
    }

    // copied from ProductFeaturesListView
    int GetNecessaryAmountOfItems(int openedAlready)
    {
        var necessaryAmountOfFeatures = 1;

        if (openedAlready == 0)
            necessaryAmountOfFeatures = 1;
        else if (openedAlready == 1)
            necessaryAmountOfFeatures = 2;
        else if (openedAlready == 2)
            necessaryAmountOfFeatures = 3;
        else
            necessaryAmountOfFeatures = openedAlready * 2;

        return necessaryAmountOfFeatures;
    }
}
