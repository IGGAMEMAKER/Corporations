using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<TaskView>().SetEntity(entity as CompanyTask);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var t1 = new CompanyTaskAcquisition(1);
        t1.Expires(15);

        var t2 = new CompanyTaskExploreMarket(NicheType.GamingBetting);
        t2.Expires(22);

        var tasks = new CompanyTask[] { t1, t2 };

        SetItems(tasks);
    }
}
