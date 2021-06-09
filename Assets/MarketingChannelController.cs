using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketingChannelController : ButtonController
{
    public ChannelInfo ChannelInfo;

    public void SetEntity(ChannelInfo channelInfo)
    {
        ChannelInfo = channelInfo;
    }

    public override void Execute()
    {
        var channel = Markets.GetMarketingChannel(Q, ChannelInfo.ID);

        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        var channelId = ChannelInfo.ID;

        var task = new TeamTaskChannelActivity(channelId, Marketing.GetChannelCost(Flagship, channelId));

        relay.AddPendingTask(task);

        var cost = Marketing.GetChannelCost(Flagship, channelId);
        Animate(Visuals.Negative($"-{Format.Money(cost)}"));

        //Marketing.EnableChannelActivity(Flagship, channel);
    }
}
