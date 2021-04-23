using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCompanyTasksListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var task = entity as TeamTask;
        var bar = t.GetComponent<ProgressBar>();

        bar.SetValue(CurrentIntDate - task.StartDate, task.EndDate);
        bar.SetCustomText(task.GetPrettyName());
    }

    public override void ViewRender()
    {
        base.ViewRender();

        List<TeamTask> tasks = Products.GetTeamTasks(Q, Flagship);

        SetItems(tasks);
    }
}
