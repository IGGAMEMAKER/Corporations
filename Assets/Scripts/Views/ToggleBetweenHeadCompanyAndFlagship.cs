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
        var isAlreadyOnMainCompanyScreen = SelectedCompany.company.Id == MyCompany.company.Id;


        var flagship = Companies.GetFlagship(Q, MyCompany);
        if (flagship != null) // && isAlreadyOnMainCompanyScreen
        {
            NavigateToProjectScreen(flagship.company.Id);

            if (TopPanelManager != null)
                TopPanelManager.PanelAnim(5);

            return;
        }

        NavigateToCompany(ScreenMode.ProjectScreen, MyCompany.company.Id);
    }
}
