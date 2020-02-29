using Assets.Core;

public class HireWorker : ButtonController
{
    public int companyId;

    public override void Execute()
    {
        var flaghship = Companies.GetFlagship(Q, MyCompany);
        var company = flaghship; // CurrentScreen == ScreenMode.GroupManagementScreen ? Companies.Get(Q, companyId) : SelectedCompany;

        Teams.HireRegularWorker(company, WorkerRole.Programmer);
    }
}
