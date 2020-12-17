using System.Collections.Generic;
using UnityEngine;

public class ManagerListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        KeyValuePair<int, WorkerRole> pair = (KeyValuePair<int, WorkerRole>)(object)entity;

        //t.GetComponent<WorkerView>().SetEntity(pair.Key, pair.Value);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        SetItems(company.team.Managers);
    }
}
