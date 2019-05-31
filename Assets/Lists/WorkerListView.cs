using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkerListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = (KeyValuePair<int, WorkerRole>)(object)entity;

        if (t.GetComponent<WorkerView>() != null)
            t.GetComponent<WorkerView>().SetEntity(e.Key, e.Value);
    }

    void Render()
    {
        if (MyProductEntity == null)
            return;

        SetItems(MyProductEntity.team.Workers.ToArray());
    }

    void OnEnable()
    {
        Render();
    }
}