using Assets.Core;
using System.Collections.Generic;

public class EmployeeListView : StaffListView
{
    public override bool DrawAsEmployee() => true;

    public override Dictionary<int, WorkerRole> Workers()
    {
        bool isOnHoldingScreen = CurrentScreen == ScreenMode.HoldingScreen;

        var company = isOnHoldingScreen ? Companies.GetFlagship(Q, MyCompany) : SelectedCompany;

        return company.employee.Managers;
    }
}
