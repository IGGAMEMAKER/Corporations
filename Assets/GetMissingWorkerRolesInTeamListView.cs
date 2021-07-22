using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetMissingWorkerRolesInTeamListView : ListView
{
    public CandidatesManager CandidatesManager;

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<MissingWorkerRoleView>().SetEntity((WorkerRole)(object)entity);
    }

    IEnumerable<WorkerRole> GetMissingRoles()
    {
        var team = Flagship.team.Teams[SelectedTeam];

        var hasProgrammers = team.Roles.Values.Any(r => r == WorkerRole.Programmer);
        var hasMarketers = team.Roles.Values.Any(r => r == WorkerRole.Marketer);

        if (hasProgrammers && hasMarketers)
        {
            return Teams.GetMissingRoles(team);
        }
        else
        {
            return new List<WorkerRole>() { WorkerRole.Marketer, WorkerRole.Programmer };
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var missingRoles = GetMissingRoles();

        SetItems(missingRoles);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        CandidatesManager.ChoseRole(GetMissingRoles().ToList()[ind]);
    }
}