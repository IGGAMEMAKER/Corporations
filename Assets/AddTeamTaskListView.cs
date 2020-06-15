using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTeamTaskListView : ListView
{
    public int TeamId;
    public int FreeSlots;

    public void SetEntity(int teamId)
    {
        TeamId = teamId;

        ViewRender();
    }

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<AddTaskForTheTeamController>().SetEntity(TeamId, 1);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(new int[FreeSlots]);
    }
}
