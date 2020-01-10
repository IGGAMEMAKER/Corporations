using Assets.Core;
using System.Linq;
using UnityEngine;

public class CompanyTaskListVew : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<TaskView>().SetEntity((entity as GameEntity).task);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var companyId = SelectedCompany.company.Id;

        var tasks = Cooldowns.GetTasks(GameContext)
            .Where(t => t.task.CompanyTask.CompanyId == companyId)
            ;

        SetItems(tasks);
    }
}
