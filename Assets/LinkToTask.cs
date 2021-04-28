using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LinkToTask : ButtonController
{
    TeamTask TeamTask;
    int ID;

    public override void Execute()
    {
        SetParameter("TaskID", ID);
        OpenUrl("/Holding/TaskTab");
        Debug.Log("Task: " + TeamTask.GetPrettyName());
    }

    internal void SetTask(TeamTask task, int ID)
    {
        TeamTask = task;
        this.ID = ID;
    }
}