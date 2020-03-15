﻿using Assets.Core;
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
    public GameObject CompetitorsTab;
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
        var isIndependentCompany = company.isIndependentCompany;

        // player has Released products
        var hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

        var playerCanExploreAdvancedTabs = hasReleasedProducts;

        DevTab          .SetActive(isFlagshipScreen);
        UpgradesTab     .SetActive(false && isFlagshipScreen);
        PartnersTab     .SetActive(playerCanExploreAdvancedTabs && isIndependentCompany);
        InvestorsTab    .SetActive(isIndependentCompany);

        InfoTab         .SetActive(playerCanExploreAdvancedTabs);
        EconomyTab      .SetActive(playerCanExploreAdvancedTabs);
        CompetitorsTab  .SetActive(playerCanExploreAdvancedTabs);


        
        // if was on product tab and then switched to group, open info tab
        var index = TopPanelManager.currentPanelIndex;

        if (index == 5 && !isFlagshipScreen)
        {
            TopPanelManager.PanelAnim(0);
        }
    }
}
