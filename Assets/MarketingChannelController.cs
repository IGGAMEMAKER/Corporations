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

        var product = Flagship;

        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        var channelId = ChannelInfo.ID;

        if (Marketing.IsActiveInChannel(product, channelId))
        {
            Debug.Log("Is active already");
            return;
        }

        var active = Marketing.GetActiveChannelsCount(product);
        var limit = Marketing.GetActiveChannelsLimit(product);

        if (active >= limit)
        {
            NotificationUtils.AddSimplePopup(Q, "Hire more marketers!");
            Debug.Log("Too many channels??");

            return;
        }

        var task = new TeamTaskChannelActivity(channelId, Marketing.GetChannelCost(product, channelId));

        relay.AddPendingTask(task);

        var cost = Marketing.GetChannelCost(product, channelId);
        Animate(Visuals.Negative($"-{Format.Money(cost)}"));

        //Marketing.EnableChannelActivity(product, channel);
    }
}
