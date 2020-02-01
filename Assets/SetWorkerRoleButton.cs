using Assets.Core;

public class SetWorkerRoleButton : ButtonController
{
    WorkerRole WorkerRole;

    public override void Execute()
    {
        Teams.SetRole(MyCompany, SelectedHuman.human.Id, WorkerRole, Q);
    }

    public void SetRole(WorkerRole workerRole)
    {
        WorkerRole = workerRole;
    }
}
