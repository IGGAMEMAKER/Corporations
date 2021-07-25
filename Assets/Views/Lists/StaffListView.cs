using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StaffListView : ListView
{
    public abstract Dictionary<int, WorkerRole> Workers();

    public override void SetItem<T>(Transform t, T entity)
    {
        var e = (KeyValuePair<int, WorkerRole>)(object)entity;

        t.GetComponent<WorkerView>().SetEntity(e.Key, e.Value);

        if (makeTransferrable && !t.GetComponent<TransferWorker>())
            t.gameObject.AddComponent<TransferWorker>();
    }

    bool makeTransferrable => GetComponent<TransferrableWorkers>() != null;

    public override void ViewRender()
    {
        base.ViewRender();

        var items = Workers().OrderByDescending(OrderWorkers);

        SetItems(items);
    }

    Func<KeyValuePair<int, WorkerRole>, int> OrderWorkers = p =>
    {
        //return 1;
        var worker = Humans.Get(Q, p.Key);
        var employeeBonus = Humans.IsEmployed(worker) ? 0 : 5000;

        return employeeBonus + Teams.GetWorkerOrder(p.Value) * 100 + Humans.GetRating(Q, p.Key);
    };
}