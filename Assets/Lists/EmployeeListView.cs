using System.Collections.Generic;

public class EmployeeListView : StaffListView
{
    public override Dictionary<int, WorkerRole> Workers()
    {
        return SelectedCompany.employee.Managers;
    }
}
