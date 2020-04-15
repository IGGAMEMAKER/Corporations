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

    public GameObject CorporateCulture;
    public GameObject Investments;
    public GameObject Messages;

    //public TopPanelManager TopPanelManager;


    public override void ViewRender()
    {
        base.ViewRender();


        var hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

        var playerCanExploreAdvancedTabs = hasReleasedProducts;

        var daughters = Companies.GetDaughterProductCompanies(Q, MyCompany);
        var numberOfDaughters = daughters.Length;

        var operatingMarkets = GetOperatingMarkets(daughters);


        DevTab.SetActive(true);
        TeamTab.SetActive(playerCanExploreAdvancedTabs);
        GroupTab.SetActive(numberOfDaughters > 1 && operatingMarkets.Count > 1);
        ExpansionTab.SetActive(playerCanExploreAdvancedTabs);


        CorporateCulture.SetActive(numberOfDaughters > 1 || Flagship.team.Managers.Count > 1);
        Investments.SetActive(playerCanExploreAdvancedTabs);
        Messages.SetActive(false && playerCanExploreAdvancedTabs);
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
