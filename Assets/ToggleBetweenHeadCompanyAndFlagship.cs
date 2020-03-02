using Assets.Core;
using Michsky.UI.Frost;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBetweenHeadCompanyAndFlagship : ButtonController
{
    public TopPanelManager TopPanelManager;

    public override void Execute()
    {
        var companyId = MyCompany.company.Id;

        var isAlreadyOnMainCompanyScreen = SelectedCompany.company.Id == companyId;


        var flagship = Companies.GetFlagship(Q, MyCompany);
        if (flagship != null && isAlreadyOnMainCompanyScreen)
        {
            NavigateToProjectScreen(flagship.company.Id);

            if (TopPanelManager != null)
                TopPanelManager.PanelAnim(5);

            return;
        }

        NavigateToCompany(ScreenMode.ProjectScreen, companyId);
    }
}
