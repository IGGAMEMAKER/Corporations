using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTasksListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<TeamTaskView>().SetEntity((TeamTask)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(Products.GetTeamTasks(Q, Flagship));
    }
}
