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

        var screen = CurrentScreen;
        bool isMyCompanyScreen = screen == ScreenMode.GroupManagementScreen || (SelectedCompany.company.Id == MyCompany.company.Id && screen == ScreenMode.ProjectScreen);

        bool hasAtLeastOneCompany = Companies.IsHasDaughters(Q, MyCompany);
        bool hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

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
