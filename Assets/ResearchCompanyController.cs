using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchCompanyController : ButtonController
{
    public override void Execute()
    {
        CooldownUtils.AddTask(GameContext, new CompanyTaskExploreCompany(SelectedCompany.company.Id), 8);
        //Navigate(ScreenMode.GroupManagementScreen);
        //SelectedCompany.AddResearch(1);
    }
}
