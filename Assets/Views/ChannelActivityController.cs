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

        //var teamId = relay.ChosenTeamId;
        //var taskId = relay.ChosenSlotId;

        var channelId = channel.marketingChannel.ChannelInfo.ID;

        var task = new TeamTaskChannelActivity(channelId);
        var teamId = Teams.GetTeamIdForTask(Flagship, task);

        var taskId = 0;

        if (teamId == -1)
        {
            teamId = Teams.AddTeam(Flagship, TeamType.CrossfunctionalTeam);
            taskId = 0;
        }
        else
        {
            taskId = Flagship.team.Teams[teamId].Tasks.Count;
        }

        Marketing.ToggleChannelActivity(company, Q, channel, teamId, taskId);



        relay.ChooseWorkerInteractions();
        MarketingChannelView.ViewRender();
    }
}
