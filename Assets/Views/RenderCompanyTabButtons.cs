using Assets.Core;
using Michsky.UI.Frost;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderCompanyTabButtons : View
{
    public GameObject ManagersTab;
    public GameObject EconomyTab;
    public GameObject InvestorsTab;
    public GameObject PartnersTab;
    public GameObject CompetitorsTab;
    public GameObject InfoTab;


    public TopPanelManager TopPanelManager;

    public override void ViewRender()
    {
        base.ViewRender();

        var company = SelectedCompany;

        var flagshipId = Companies.GetPlayerFlagshipID(Q);

        var isMyCompanyScreen = company.company.Id == MyCompany.company.Id;
        var isFlagshipScreen = company.company.Id == flagshipId;

        var hasProducts = Companies.GetDaughterProductCompanies(Q, MyCompany).Count() > 0;
        var isIndependentCompany = company.isIndependentCompany;

        // player has Released products
        var hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

        var playerCanExploreAdvancedTabs = hasReleasedProducts;

        var daughters = Companies.GetDaughterProductCompanies(Q, MyCompany);
        var numberOfDaughters = daughters.Length;

        if (ManagersTab != null)
            ManagersTab     .SetActive(playerCanExploreAdvancedTabs);

        if (PartnersTab != null)
            PartnersTab     .SetActive(playerCanExploreAdvancedTabs && isIndependentCompany);
        if (InvestorsTab != null)
            InvestorsTab    .SetActive(isIndependentCompany);

        if (InfoTab != null)
            InfoTab         .SetActive(playerCanExploreAdvancedTabs);
        if (EconomyTab != null)
            EconomyTab      .SetActive(playerCanExploreAdvancedTabs);
        if (CompetitorsTab != null)
            CompetitorsTab  .SetActive(false && playerCanExploreAdvancedTabs);

        // not necessary, cause moved dev panel to separate screen
        //// if was on product tab and then switched to group, open info tab
        //var index = TopPanelManager.currentPanelIndex;

        //if (index == 5 && !isFlagshipScreen)
        //{
        //    TopPanelManager.PanelAnim(0);
        //}
    }
}
