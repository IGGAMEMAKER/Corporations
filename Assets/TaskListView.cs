using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<TaskView>().SetEntity((entity as GameEntity).task);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var t1 = new CompanyTaskAcquisition(1);
        var t2 = new CompanyTaskExploreMarket(NicheType.GamingBetting);

        //var tasks = new CompanyTask[] { t1, t2 };
        var tasks = CooldownUtils.GetTasks(GameContext);

        SetItems(tasks);
    }
}
