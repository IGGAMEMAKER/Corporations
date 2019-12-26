using Assets.Core;

public class RenderCMO : View
{
    void OnEnable()
    {
        var role = WorkerRole.MarketingDirector;

        var workerId = TeamUtils.GetWorkerByRole(SelectedCompany, GameContext, role);
        GetComponent<WorkerView>().SetEntity(workerId, role);
    }
}
