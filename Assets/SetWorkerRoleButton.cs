using Assets.Utils;

public class SetWorkerRoleButton : ButtonController
{
    WorkerRole WorkerRole;

    public override void Execute()
    {
        TeamUtils.SetRole(MyProductEntity, SelectedHuman.human.Id, WorkerRole, GameContext);
    }

    public void SetRole(WorkerRole workerRole)
    {
        WorkerRole = workerRole;
    }
}
