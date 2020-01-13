using Assets.Core;

public class HireWorker : ButtonController
{
    public override void Execute()
    {
        TeamUtils.HireRegularWorker(SelectedCompany, WorkerRole.Programmer);
    }
}
