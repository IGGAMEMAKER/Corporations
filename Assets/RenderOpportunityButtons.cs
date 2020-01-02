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

        bool isMyCompanyScreen = SelectedCompany.company.Id == MyCompany.company.Id && CurrentScreen == ScreenMode.ProjectScreen;

        bool hasAtLeastOneCompany = Companies.IsHasDaughters(GameContext, MyCompany);
        bool hasReleasedProducts = Companies.IsHasReleasedProducts(GameContext, MyCompany);

        Partnerships
            .SetActive(isMyCompanyScreen && hasReleasedProducts);
        PerspectiveMarkets
            .SetActive(isMyCompanyScreen && hasReleasedProducts);
        CorporateCulture
            .SetActive(isMyCompanyScreen && hasReleasedProducts);

        Acquisitions.SetActive(isMyCompanyScreen && false);
        RaiseInvestmentsButton.SetActive(isMyCompanyScreen && hasAtLeastOneCompany);
    }
}
