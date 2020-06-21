using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamTaskListView : ListView
{
    public int TeamId;
    public int ChosenSlots;

    public void SetEntity(int teamId)
    {
        TeamId = teamId;

        ViewRender();
    }

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<TeamTaskView>().SetEntity(TeamId, index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (Flagship.team.Teams.Count > TeamId)
            SetItems(new int[ChosenSlots]);
    }
}
