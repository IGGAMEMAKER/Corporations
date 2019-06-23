using Assets.Utils;
using System;
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

    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(SelectedCompany.team.Workers.OrderBy(p => - (GetWorkerOrder(p.Value) * 1000 + HumanUtils.GetOverallRating(p.Key, GameContext))).ToArray());
    }

    Func<WorkerRole, int> GetWorkerOrder = role =>
    {
        if (role == WorkerRole.Business)
            return 10;

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

        if (role == WorkerRole.Universal)
            return 1;

        return 0;
    };

    int CompareWorkerRoles (WorkerRole role1, WorkerRole role2)
    {
        var first = -1;
        var second = 1;

        if (role1 == WorkerRole.Business)
            return first;

        if (role2 == WorkerRole.Business)
            return second;


        if (role1 == WorkerRole.TechDirector)
            return first;

        if (role2 == WorkerRole.TechDirector)
            return second;


        if (role1 == WorkerRole.MarketingDirector)
            return first;

        if (role2 == WorkerRole.MarketingDirector)
            return second;


        if (role1 == WorkerRole.TeamLead)
            return first;

        if (role2 == WorkerRole.TeamLead)
            return second;


        if (role1 == WorkerRole.MarketingLead)
            return first;

        if (role2 == WorkerRole.MarketingLead)
            return second;


        if (role1 == WorkerRole.ProjectManager)
            return first;

        if (role2 == WorkerRole.ProjectManager)
            return second;


        if (role1 == WorkerRole.ProductManager)
            return first;

        if (role2 == WorkerRole.ProductManager)
            return second;


        if (role1 == WorkerRole.Marketer)
            return first;

        if (role2 == WorkerRole.Marketer)
            return second;


        if (role1 == WorkerRole.Programmer)
            return first;

        if (role2 == WorkerRole.Programmer)
            return second;


        if (role1 == WorkerRole.Universal)
            return first;

        if (role2 == WorkerRole.Universal)
            return second;

        return 0;
    }
}