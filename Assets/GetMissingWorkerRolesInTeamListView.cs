using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core;
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
        
        var roles = Teams.GetRolesForTeam(team.TeamType);
        var missingRoles = roles.Where(r => !Teams.HasRole(r, team, Q));
        
        SetItems(missingRoles);
    }
}