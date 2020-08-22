using System.Collections.Generic;

public class WorkerListView : StaffListView
{
    public bool IncludeEmployees = false;

    public override Dictionary<int, WorkerRole> Workers()
    {
        var c = GetCompany();

        var managers = c.team.Managers;
        var employees = c.employee.Managers;

        var dict = new Dictionary<int, WorkerRole>();

        foreach (var kvp in managers)
            dict[kvp.Key] = kvp.Value;

        if (IncludeEmployees)
        {
            foreach (var kvp in employees)
                dict[kvp.Key] = kvp.Value;
        }

        return dict;
    }

    GameEntity GetCompany()
    {
        bool isOnHoldingScreen = CurrentScreen == ScreenMode.HoldingScreen;

        var company = isOnHoldingScreen ? Flagship : SelectedCompany;

        return company;
    }
}
