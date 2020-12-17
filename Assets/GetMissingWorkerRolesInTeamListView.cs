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
        
        var missingRoles = Teams.GetMissingRoles(team);
        
        SetItems(missingRoles);
    }
}