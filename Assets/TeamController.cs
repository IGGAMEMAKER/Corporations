using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamController : BaseCommandHandler
{
    public override void HandleCommand(string eventName, Dictionary<string, object> parameters)
    {
        switch (eventName)
        {
            case Commands.TEAM_WORKERS_HIRE:
                Hire(parameters);
                break;

            case Commands.TEAM_WORKERS_FIRE:
                Fire(parameters);
                break;
        }
    }

    private void Fire(Dictionary<string, object> parameters)
    {
        int workerId = (int)parameters["workerId"];
        int projectId = (int)parameters["projectId"];

        application.Fire(projectId, workerId);
    }

    private void Hire(Dictionary<string, object> parameters)
    {
        int workerId = (int)parameters["workerId"];
        int projectId = (int)parameters["projectId"];

        application.Hire(projectId, workerId);
    }
}
