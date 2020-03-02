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

    public TopPanelManager TopPanelManager;

    public override void ViewRender()
    {
        base.ViewRender();

        var company = SelectedCompany;
        var flagship = Companies.GetFlagship(Q, MyCompany);
        var hasFlagship = flagship != null;

        var isMyCompanyScreen = company.company.Id == MyCompany.company.Id;
        var isFlagshipScreen = hasFlagship && company.company.Id == flagship.company.Id;

        var hasProducts = Companies.GetDaughterProductCompanies(Q, MyCompany).Count() > 0;

        DevTab.SetActive(isFlagshipScreen);

        // if was on product tab and then switched to group, open info tab
        var index = TopPanelManager.currentPanelIndex;

        if (index == 5 && !isMyCompanyScreen)
            TopPanelManager.PanelAnim(0);

        var isIndependentCompany = company.isIndependentCompany;

        PartnersTab.SetActive(isIndependentCompany);
        InvestorsTab.SetActive(isIndependentCompany);

    }
}
