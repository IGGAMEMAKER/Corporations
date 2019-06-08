using System.Collections.Generic;

public class ListenTeamChanges : Controller
    , ITeamListener
{
    public override void AttachListeners()
    {
        //if (HasProductCompany)
        SelectedCompany.AddTeamListener(this);
    }

    public override void DetachListeners()
    {
        //if (HasProductCompany)
        SelectedCompany.RemoveTeamListener(this);
    }

    void ITeamListener.OnTeam(GameEntity entity, int morale, Dictionary<int, WorkerRole> workers, TeamStatus teamStatus)
    {
        Render();
    }
}
