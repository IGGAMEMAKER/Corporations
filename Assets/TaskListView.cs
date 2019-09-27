using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<TaskView>().SetEntity((CompanyTask)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var tasks = new CompanyTask[]
        {
            new CompanyTaskAcquisition(),
            new CompanyTaskExploreMarket(NicheType.GamingBetting)
        };

        SetItems(tasks);
    }
}
