using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTeamTaskListView : ListView
{
    public int TeamId;
    public int FreeSlots;

    int TasksCount => Flagship.team.Teams[TeamId].Tasks.Count;

    public void SetEntity(int teamId)
    {
        TeamId = teamId;

        ViewRender();
    }

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<AddTaskForTheTeamController>().SetEntity(TeamId);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        FreeSlots = Mathf.Max(C.TASKS_PER_TEAM - TasksCount, 0);

        SetItems(new int[FreeSlots]);
    }
}
