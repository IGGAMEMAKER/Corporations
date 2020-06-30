using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CandidatesForRoleListView : ListView
{
    public WorkerRole WorkerRole;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var id = (int)(object)entity;

        t.GetComponent<WorkerView>().SetEntity(id, WorkerRole);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var competitors = Companies.GetCompetitorsOfCompany(company, Q, false);

        Debug.Log("Competitors: " + string.Join(", ", competitors.Select(c => c.company.Name)));

        var managers = new List<GameEntity>();
        var managerIds = new List<int>();

        managerIds.AddRange(company.employee.Managers.Where(p => p.Value == WorkerRole).Select(p => p.Key));

        foreach (var c in competitors)
        {
            var workers = c.team.Managers.Where(p => p.Value == WorkerRole).Select(p => p.Key);
            managerIds.AddRange(workers);
        }

        SetItems(managerIds); // .Select(id => Humans.GetHuman(Q, id))
    }
}
