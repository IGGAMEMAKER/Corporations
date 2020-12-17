using Assets.Core;

public class SpawnEmplyeesByWorkerRole : ButtonController
{
    public override void Execute()
    {
        Teams.ShaffleEmployees(SelectedCompany, Q);
    }
}
