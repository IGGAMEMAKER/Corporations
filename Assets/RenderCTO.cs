using Assets.Core;

public class RenderCTO : View
{
    void OnEnable()
    {
        var role = WorkerRole.TechDirector;

        var workerId = TeamUtils.GetWorkerByRole(SelectedCompany, GameContext, role);
        GetComponent<WorkerView>().SetEntity(workerId, role);
    }
}
