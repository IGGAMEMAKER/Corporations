using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActiveTasksListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<TeamTaskView>().SetEntity((TeamTask)(object)entity, Products.GetTeamTasks(Q, Flagship).Count - 1 - index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        List<TeamTask> tasks = new List<TeamTask>();

        var t2 = Products.GetTeamTasks(Q, Flagship);

        for (var i = t2.Count - 1; i >= 0; i--)
        {
            tasks.Add(t2[i]);
        }

        //SetItems(Products.GetTeamTasks(Q, Flagship));
        SetItems(tasks);
    }
}
