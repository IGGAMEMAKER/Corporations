using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmployeeListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = (KeyValuePair<int, WorkerRole>)(object)entity;

        if (t.GetComponent<WorkerView>() != null)
            t.GetComponent<WorkerView>().SetEntity(e.Key, e.Value);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var employees = SelectedCompany.employee.Managers;

        SetItems(employees.ToArray());
    }
}
