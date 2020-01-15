using System.Collections.Generic;

public class WorkerListView : StaffListView
{
    public override Dictionary<int, WorkerRole> Workers()
    {
        return SelectedCompany.team.Managers;
    }
}
