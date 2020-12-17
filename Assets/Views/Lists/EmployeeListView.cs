using System.Collections.Generic;

public class EmployeeListView : StaffListView
{
    public override Dictionary<int, WorkerRole> Workers()
    {
        bool isOnHoldingScreen = CurrentScreen == ScreenMode.HoldingScreen;

        var company = isOnHoldingScreen ? Flagship : SelectedCompany;

        return company.employee.Managers;
    }
}
