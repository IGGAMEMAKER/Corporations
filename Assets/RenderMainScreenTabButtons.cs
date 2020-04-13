using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMainScreenTabButtons : View
{
    public GameObject DevTab;
    public GameObject TeamTab;
    public GameObject GroupTab;
    public GameObject ExpansionTab;

    //public TopPanelManager TopPanelManager;


    public override void ViewRender()
    {
        base.ViewRender();


        var hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

        var playerCanExploreAdvancedTabs = hasReleasedProducts;

        var daughters = Companies.GetDaughterProductCompanies(Q, MyCompany);
        var numberOfDaughters = daughters.Length;

        var operatingMarkets = GetOperatingMarkets(daughters);


        if (DevTab != null)
            DevTab.SetActive(true);
        if (TeamTab != null)
            TeamTab.SetActive(playerCanExploreAdvancedTabs);
        if (GroupTab != null)
            GroupTab.SetActive(numberOfDaughters > 1 && operatingMarkets.Count > 1);

        if (ExpansionTab != null)
            ExpansionTab.SetActive(playerCanExploreAdvancedTabs);

    }



    List<NicheType> GetOperatingMarkets(GameEntity[] products)
    {
        var markets = new List<NicheType>();

        foreach (var p in products)
        {
            if (!markets.Contains(p.product.Niche))
                markets.Add(p.product.Niche);
        }

        return markets;
    }
}
