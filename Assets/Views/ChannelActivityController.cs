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

        var teamId = relay.ChosenTeamId;
        var taskId = relay.ChosenSlotId;

        Marketing.ToggleChannelActivity(company, Q, channel, teamId, taskId);
        relay.ChooseWorkerInteractions();



        MarketingChannelView.ViewRender();
    }
}
