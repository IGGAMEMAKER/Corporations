using System.Collections.Generic;

public class ListenTeamChanges : Controller
    , ITeamListener
{
    public override void AttachListeners()
    {
        var company = GetFollowableCompany();

        if (company != null)
            company.AddTeamListener(this);
    }

    public override void DetachListeners()
    {
        var company = GetFollowableCompany();

        if (company != null)
            company.RemoveTeamListener(this);
    }

    void ITeamListener.OnTeam(GameEntity entity, int morale, int organisation, Dictionary<int, WorkerRole> managers, Dictionary<WorkerRole, int> workers, List<TeamInfo> teams)
    {
        Render();
    }
}
