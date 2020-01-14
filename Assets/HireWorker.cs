using Assets.Core;

public class HireWorker : ButtonController
{
    public override void Execute()
    {
        Teams.HireRegularWorker(SelectedCompany, WorkerRole.Programmer);
    }
}
