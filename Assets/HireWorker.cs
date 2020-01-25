﻿using Assets.Core;

public class HireWorker : ButtonController
{
    public int companyId;

    public override void Execute()
    {
        var company = CurrentScreen == ScreenMode.GroupManagementScreen ? Companies.GetCompany(GameContext, companyId) : SelectedCompany;

        Teams.HireRegularWorker(company, WorkerRole.Programmer);
    }
}
