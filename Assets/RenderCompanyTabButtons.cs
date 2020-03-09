using Assets.Core;
using Michsky.UI.Frost;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderCompanyTabButtons : View
{
    public GameObject DevTab;
    public GameObject TeamTab;
    public GameObject EconomyTab;
    public GameObject InvestorsTab;
    public GameObject PartnersTab;
    public GameObject InfoTab;
    public GameObject UpgradesTab;

    public TopPanelManager TopPanelManager;

    public override void ViewRender()
    {
        base.ViewRender();

        var company = SelectedCompany;

        var flagshipId = Companies.GetPlayerFlagshipID(Q);

        var isMyCompanyScreen = company.company.Id == MyCompany.company.Id;
        var isFlagshipScreen = company.company.Id == flagshipId;

        var hasProducts = Companies.GetDaughterProductCompanies(Q, MyCompany).Count() > 0;

        DevTab.SetActive(isFlagshipScreen);
        UpgradesTab.SetActive(isFlagshipScreen);

        // if was on product tab and then switched to group, open info tab
        var index = TopPanelManager.currentPanelIndex;

        if (index == 5 && !isFlagshipScreen)
            TopPanelManager.PanelAnim(0);

        var isIndependentCompany = company.isIndependentCompany;

        PartnersTab.SetActive(isIndependentCompany);
        InvestorsTab.SetActive(isIndependentCompany);
    }
}
