using System.Collections.Generic;

public class ListenTeamChanges : Controller
    , ITeamListener
{
    public override void AttachListeners()
    {
        //if (HasProductCompany)
        if (SelectedCompany != null)
            SelectedCompany.AddTeamListener(this);
    }

    public override void DetachListeners()
    {
        //if (HasProductCompany)
        if (SelectedCompany != null)
            SelectedCompany.RemoveTeamListener(this);
    }

    void ITeamListener.OnTeam(GameEntity entity, int morale, Dictionary<int, WorkerRole> workers, TeamStatus teamStatus)
    {
        Render();
    }
}
