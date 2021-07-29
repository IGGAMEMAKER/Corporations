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

        //var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        var channelId = ChannelInfo.ID;

        var task = new TeamTaskChannelActivity(channelId, Marketing.GetChannelCost(product, channelId));
        var cost = Teams.GetTaskCost(product, task, Q);

        Debug.Log("Cost " + Format.Money(cost) + " " + channelId);

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
            return;
        }

        Debug.Log("Marketing controller");
        //if (Teams.IsEnoughResourcesForTask(product, task, Q))
        if (Companies.IsEnoughResources(MyCompany, cost))
        {
            Debug.Log("Have resources");

            //relay.AddPendingTask(task);
            Teams.AddTeamTask(product, CurrentIntDate, Q, 0, task);

            Animate(Visuals.Negative($"-{Format.Money(cost)}"));
        }
    }
}
