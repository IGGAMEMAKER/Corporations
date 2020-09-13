using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CandidatesForRoleListView : ListView
{
    public WorkerRole WorkerRole;
    public Text DebugTable;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var humanId = (int)(object)entity;

        t.GetComponent<WorkerView>().SetEntity(humanId, WorkerRole);
        t.GetComponent<EmployeePreview>().SetEntity(humanId);
    }

    GameEntity company => Flagship;

    public override void ViewRender()
    {
        base.ViewRender();

        var competitors = Companies.GetCompetitorsOfCompany(company, Q, false);

        var teamId = FindObjectOfType<FlagshipRelayInCompanyView>().ChosenTeamId;
        var team = company.team.Teams[teamId];

        //Debug.Log("Competitors: " + string.Join(", ", competitors.Select(c => c.company.Name)));

        var managers = new List<GameEntity>();
        var managerIds = new List<int>();

        managerIds.AddRange(
            company.employee.Managers
            //.Where(p => p.Value == WorkerRole)
            .Where(Teams.RoleSuitsTeam(company, team))
            .Select(p => p.Key)
            );

        foreach (var c in competitors)
        {
            var workers = c.team.Managers
                .Where(Teams.RoleSuitsTeam(company, team))
                //.Where(p => p.Value == WorkerRole)
                .Select(p => p.Key);
            managerIds.AddRange(workers);
        }

        bool isCoreTeam = teamId == 0;

        SetItems(managerIds); // .Select(id => Humans.GetHuman(Q, id))
    }
}
