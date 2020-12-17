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


        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        var channelId = channel.ID;

        var task = new TeamTaskChannelActivity(channelId, Marketing.GetChannelCost(company, channelId));

        relay.AddPendingTask(task);

        var channelList = FindObjectOfType<MarketingChannelsListView>();

        if (channelList != null)
        {

            // view render to recalculate features count
            channelList.ViewRender();

            if (channelList.count == 0)
            {
                CloseModal("Marketing");
            }

            MarketingChannelView.ViewRender();
        }
    }
}
