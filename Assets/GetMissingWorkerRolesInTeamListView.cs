using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetMissingWorkerRolesInTeamListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<MissingWorkerRoleView>().SetEntity((WorkerRole)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var team = Flagship.team.Teams[SelectedTeam];

        var hasProgrammers = team.Roles.Values.Any(r => r == WorkerRole.Programmer);
        var hasMarketers = team.Roles.Values.Any(r => r == WorkerRole.Marketer);

        if (hasProgrammers && hasMarketers)
        {
            SetItems(Teams.GetMissingRoles(team));
        }
        else
        {
            SetItems(new List<WorkerRole>() { WorkerRole.Marketer, WorkerRole.Programmer });
        }
    }
}