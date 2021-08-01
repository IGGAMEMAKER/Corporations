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
        var channelId = ChannelInfo.ID;
        var product = Flagship;
        var payer = MyCompany;

        if (Marketing.IsActiveInChannel(product, channelId))
        {
            Debug.Log("Is active already");
            return;
        }

        if (Marketing.IsNeedsMoreMarketersForCampaign(product))
        {
            NotificationUtils.AddSimplePopup(Q, "Hire more marketers!");
            return;
        }

        var task = TeamTaskChannelActivity.FromChannel(ChannelInfo); // new TeamTaskChannelActivity(channelId, Marketing.GetChannelCost(product, channelId));
        var cost = Teams.GetTaskCost(product, task, Q);

        if (Companies.IsEnoughResources(payer, cost))
        {
            Teams.AddTeamTask(product, CurrentIntDate, Q, 0, task);

            Animate(Visuals.Negative($"-{Format.Money(cost)}"));
        }
    }
}
