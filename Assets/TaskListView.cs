using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<TaskView>().SetEntity((entity as GameEntity).timedAction);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var tasks = Cooldowns.GetTasks(Q);

        SetItems(tasks);
    }
}
