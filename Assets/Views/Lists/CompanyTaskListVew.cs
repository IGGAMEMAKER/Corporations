using Assets.Core;
using System.Linq;
using UnityEngine;

public class CompanyTaskListVew : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<TaskView>().SetEntity((entity as GameEntity).timedAction);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var companyId = SelectedCompany.company.Id;

        var tasks = Cooldowns.GetTasksOfCompany(Q, companyId)
            .Reverse()
            ;

        SetItems(tasks);
    }
}
