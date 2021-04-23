using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddMarketingCampaignController : ButtonController
{
    public override void Execute()
    {
        ScheduleUtils.ResumeGame(Q);

        var features = Markets.GetAffordableMarketingChannels(Flagship, Q);

        var channel = features.First();

        var task = new TeamTaskChannelActivity(channel.ID, (long)channel.costPerAd);
        Teams.AddTeamTask(Flagship, CurrentIntDate, Q, 0, task);
    }
}
