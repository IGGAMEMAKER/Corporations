using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderOpportunityButtons : View
{
    public GameObject PerspectiveMarkets;
    public GameObject RaiseInvestmentsButton;
    public GameObject Acquisitions;
    public GameObject CorporateCulture;
    public GameObject Partnerships;

    public void OnEnable()
    {
        //base.ViewRender();

        bool hasAtLeastOneCompany = Companies.IsHasDaughters(GameContext, MyCompany);
        bool hasReleasedProducts = Companies.GetDaughterProductCompanies(GameContext, MyCompany).Count(p => p.isRelease) > 0;

        Partnerships.SetActive(hasReleasedProducts);

        CorporateCulture.SetActive(hasAtLeastOneCompany);
        Acquisitions.SetActive(hasAtLeastOneCompany);
        RaiseInvestmentsButton.SetActive(hasAtLeastOneCompany);
    }
}
