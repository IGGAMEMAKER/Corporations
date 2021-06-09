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

        Marketing.EnableChannelActivity(Flagship, channel);
    }
}
