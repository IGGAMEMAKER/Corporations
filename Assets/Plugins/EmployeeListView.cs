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
    }

    private void OnEnable()
    {
        var employees = new GameEntity[0];

        SetItems(employees.ToArray());
    }
}
