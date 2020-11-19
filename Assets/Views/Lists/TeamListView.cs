using Assets.Core;
using System.Collections.Generic;

public class TeamListView : StaffListView
{
    public bool IncludeEmployees = false;

    public override Dictionary<int, WorkerRole> Workers()
    {
        var c = GetCompany();

        var chosenTeamId = SelectedTeam; // FindObjectOfType<FlagshipRelayInCompanyView>().ChosenTeamId;

        var team = c.team.Teams[chosenTeamId];

        var managers = team.Managers;

        var dict = new Dictionary<int, WorkerRole>();

        foreach (var kvp in managers)
            dict[kvp] = Humans.Get(Q, kvp).worker.WorkerRole;

        return dict;
    }

    GameEntity GetCompany()
    {
        return Flagship;
    }
}
