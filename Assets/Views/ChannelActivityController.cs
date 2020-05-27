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

        Marketing.ToggleChannelActivity(Flagship, Q, channel);

        MarketingChannelView.ViewRender();
    }
}
