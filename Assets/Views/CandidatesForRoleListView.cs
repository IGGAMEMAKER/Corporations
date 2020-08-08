using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CandidatesForRoleListView : ListView
{
    public WorkerRole WorkerRole;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var humanId = (int)(object)entity;

        t.GetComponent<WorkerView>().SetEntity(humanId, WorkerRole);
        t.GetComponent<EmployeePreview>().SetEntity(humanId);
    }

    GameEntity company => Flagship;

    WorkerRole[] GetRolesForTeam(TeamType teamType)
    {
        switch (teamType)
        {
            default:
            case TeamType.DevelopmentTeam: return new WorkerRole[] { WorkerRole.ProductManager, WorkerRole.TeamLead, WorkerRole.ProjectManager };
        }
    }

    public Func<KeyValuePair<int, WorkerRole>, bool> roleSuitsTeam(GameEntity company) => pair => IsRoleSuitsTeam(pair.Value, company);
    public bool IsRoleSuitsTeam (WorkerRole workerRole, GameEntity company)
    {
        var team = company.team.Teams[0];

        return true;
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var competitors = Companies.GetCompetitorsOfCompany(company, Q, false);

        //Debug.Log("Competitors: " + string.Join(", ", competitors.Select(c => c.company.Name)));

        var managers = new List<GameEntity>();
        var managerIds = new List<int>();

        managerIds.AddRange(
            company.employee.Managers
            //.Where(p => p.Value == WorkerRole)
            .Where(roleSuitsTeam(company))
            .Select(p => p.Key)
            );

        foreach (var c in competitors)
        {
            var workers = c.team.Managers
                .Where(roleSuitsTeam(company))
                //.Where(p => p.Value == WorkerRole)
                .Select(p => p.Key);
            managerIds.AddRange(workers);
        }

        SetItems(managerIds); // .Select(id => Humans.GetHuman(Q, id))
    }
}
