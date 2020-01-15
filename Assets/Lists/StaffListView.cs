using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StaffListView : ListView
{
    public abstract Dictionary<int, WorkerRole> Workers();

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = (KeyValuePair<int, WorkerRole>)(object)entity;

        //if (t.GetComponent<WorkerView>() != null)
        t.GetComponent<WorkerView>().SetEntity(e.Key, e.Value);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var items = Workers()
            .OrderByDescending(OrderWorkers)
            ;

        SetItems(items);
    }

    Func<KeyValuePair<int, WorkerRole>, int> OrderWorkers = p =>
    {
        //return 1;
        return GetWorkerOrder(p.Value) * 1000 + Humans.GetRating(GameContext, p.Key);
    };

    static int GetWorkerOrder(WorkerRole role)
    {
        if (role == WorkerRole.CEO)
            return 15;

        if (role == WorkerRole.Universal)
            return 11;

        if (role == WorkerRole.TechDirector)
            return 9;

        if (role == WorkerRole.MarketingDirector)
            return 8;

        if (role == WorkerRole.TeamLead)
            return 7;

        if (role == WorkerRole.MarketingLead)
            return 6;

        if (role == WorkerRole.ProjectManager)
            return 5;

        if (role == WorkerRole.ProductManager)
            return 4;

        if (role == WorkerRole.Marketer)
            return 3;

        if (role == WorkerRole.Programmer)
            return 2;

        return 0;
    }
}