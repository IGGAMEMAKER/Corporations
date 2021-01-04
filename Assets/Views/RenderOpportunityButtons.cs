using Assets.Core;
using UnityEngine;

public class RenderOpportunityButtons : View
{
    public GameObject PerspectiveMarkets;
    public GameObject RaiseInvestmentsButton;
    public GameObject CorporateCulture;
    public GameObject Partnerships;

    //public void OnEnable()
    public override void ViewRender()
    {
        base.ViewRender();
        //base.ViewRender();

        var screen = CurrentScreen;
        bool isMyCompanyScreen = true; // screen == ScreenMode.GroupManagementScreen || (SelectedCompany.company.Id == MyCompany.company.Id && screen == ScreenMode.ProjectScreen);

        bool hasAtLeastOneCompany = Companies.IsHasDaughters(MyCompany);
        bool hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

        Partnerships
            .SetActive(isMyCompanyScreen && hasReleasedProducts);

        PerspectiveMarkets
            .SetActive(isMyCompanyScreen && hasReleasedProducts);

        CorporateCulture
            .SetActive(isMyCompanyScreen && hasReleasedProducts);

        RaiseInvestmentsButton.SetActive(isMyCompanyScreen && hasAtLeastOneCompany);
    }
}
