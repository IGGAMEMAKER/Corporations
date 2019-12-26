using Assets.Core;
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
        bool hasReleasedProducts = Companies.IsHasReleasedProducts(GameContext, MyCompany);

        Partnerships.SetActive(hasReleasedProducts);

        PerspectiveMarkets.SetActive(hasReleasedProducts);

        CorporateCulture.SetActive(hasReleasedProducts);
        Acquisitions.SetActive(false);
        RaiseInvestmentsButton.SetActive(hasAtLeastOneCompany);
    }
}
