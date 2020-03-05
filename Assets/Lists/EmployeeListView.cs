using System.Collections.Generic;

public class EmployeeListView : StaffListView
{
    public override bool DrawAsEmployee() => true;

    public override Dictionary<int, WorkerRole> Workers()
    {
        return SelectedCompany.employee.Managers;
    }
}
