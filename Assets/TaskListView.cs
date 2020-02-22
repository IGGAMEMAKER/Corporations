using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        var daughters = Companies.GetDaughterCompanies(Q, MyCompany);

        if (daughters.Length > 0)
        {
            var tasks = Cooldowns.GetCooldowns(Q, daughters[0].company.Id);

            Debug.Log("Tasks Count " + tasks.Count());

            SetItems(tasks);
        }
        else
        {
            SetItems(new GameEntity[0]);
        }

    }
}
