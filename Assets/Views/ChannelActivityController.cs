using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelActivityController : ButtonController
{
    public MarketingChannelView MarketingChannelView;

    public override void Execute()
    {
        var channel = MarketingChannelView.channel;

        var company = Flagship;

        // explore channel button
        if (channel == null)
        {
            company.channelExploration.AmountOfExploredChannels++;

            GetComponentInParent<MarketingChannelsListView>().ViewRender();

            // increase amount of possible channels
            return;
        }


        if (Marketing.IsChannelExplored(channel, company))
        {
            Marketing.ToggleChannelActivity(company, Q, channel);
        }
        else
        {
            Marketing.ExploreChannel(channel, company);
        }
        MarketingChannelView.ViewRender();
    }
}
