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

    IEnumerable<WorkerRole> GetMissingRoles() => Teams.GetMissingRolesFull(Flagship.team.Teams[SelectedTeam]);

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