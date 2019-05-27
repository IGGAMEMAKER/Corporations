using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = (int)(object)entity;

        if (t.GetComponent<WorkerView>() != null)
            t.GetComponent<WorkerView>().SetEntity(e);
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