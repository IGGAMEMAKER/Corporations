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

    WorkerRole[] GetRolesForTeam(TeamType teamType)
    {
        switch (teamType)
        {
            case TeamType.SupportTeam:
            case TeamType.MarketingTeam: return new WorkerRole[] { WorkerRole.ProjectManager, WorkerRole.MarketingLead };

            case TeamType.DevelopmentTeam: return new WorkerRole[] { WorkerRole.TeamLead, WorkerRole.ProductManager, WorkerRole.ProjectManager };
            case TeamType.DevOpsTeam: return new WorkerRole[] { WorkerRole.TeamLead };

            default:
                return new WorkerRole[] { WorkerRole.ProjectManager, WorkerRole.TeamLead, WorkerRole.MarketingLead, WorkerRole.ProductManager };
        }
    }

    public Func<KeyValuePair<int, WorkerRole>, bool> RoleSuitsTeam(GameEntity company, TeamInfo team) => pair => IsRoleSuitsTeam(pair.Value, company, team);
    public bool IsRoleSuitsTeam (WorkerRole workerRole, GameEntity company, TeamInfo team)
    {
        return GetRolesForTeam(team.TeamType).Contains(workerRole);
    }

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
            .Where(RoleSuitsTeam(company, team))
            .Select(p => p.Key)
            );

        foreach (var c in competitors)
        {
            var workers = c.team.Managers
                .Where(RoleSuitsTeam(company, team))
                //.Where(p => p.Value == WorkerRole)
                .Select(p => p.Key);
            managerIds.AddRange(workers);
        }

        SetItems(managerIds); // .Select(id => Humans.GetHuman(Q, id))
    }
}
