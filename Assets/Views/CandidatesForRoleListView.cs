using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class CandidatesForRoleListView : ListView
{
    public WorkerRole WorkerRole;
    public Text DebugTable;

    public override void SetItem<T>(Transform t, T entity)
    {
        var humanId = (int)(object)entity;

        t.GetComponent<WorkerView>().SetEntity(humanId, WorkerRole);
        t.GetComponent<EmployeePreview>().SetEntity(humanId);
    }

    GameEntity company => Flagship;

    public override void ViewRender()
    {
        base.ViewRender();

        //var teamId = FindObjectOfType<FlagshipRelayInCompanyView>().ChosenTeamId;
        var team = company.team.Teams[SelectedTeam];

        var managerIds = Teams.GetCandidatesForTeam(company, team, Q);

        SetItems(managerIds);
    }
}
