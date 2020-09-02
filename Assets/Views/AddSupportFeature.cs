using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSupportFeature : ButtonController
{
    public override void Execute()
    {
        var supportFeature = GetComponent<SupportView>().SupportFeature;
        var name = supportFeature.Name;

        var product = Flagship;

        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        //var teamId = relay.ChosenTeamId;
        //var taskId = relay.ChosenSlotId;

        var task = new TeamTaskSupportFeature(supportFeature);
        //var teamId = Teams.GetTeamIdForTask(Flagship, task);

        //var taskId = 0;

        //if (teamId == -1)
        //{
        //    teamId = Teams.AddTeam(product, TeamType.CrossfunctionalTeam);
        //    taskId = 0;
        //}
        //else
        //{
        //    taskId = Flagship.team.Teams[teamId].Tasks.Count;
        //}

        relay.AddPendingTask(task);
        //Teams.AddTeamTask(product, Q, teamId, taskId, task);
        //relay.ChooseWorkerInteractions();
    }
}
