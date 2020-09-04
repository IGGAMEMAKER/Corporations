using Assets.Core;
using System.Collections.Generic;

public class TeamListView : StaffListView
{
    public bool IncludeEmployees = false;

    public override Dictionary<int, WorkerRole> Workers()
    {
        var c = GetCompany();

        var chosenTeamId = FindObjectOfType<FlagshipRelayInCompanyView>().ChosenTeamId;

        var team = c.team.Teams[chosenTeamId];

        var managers = team.Managers;

        var dict = new Dictionary<int, WorkerRole>();

        foreach (var kvp in managers)
            dict[kvp] = Humans.GetHuman(Q, kvp).worker.WorkerRole;

        return dict;
    }

    GameEntity GetCompany()
    {
        return Flagship;
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);


    }
}
