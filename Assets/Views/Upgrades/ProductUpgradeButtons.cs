using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductUpgradeButtons : RoleRelatedButtons
{
    public ReleaseApp ReleaseApp;
    public GameObject RaiseInvestments;

    void Render()
    {
        var company = Flagship;
        var id = company.company.Id;
        
        ReleaseApp.SetCompanyId(id);

        // prerelease stuff
        // ---------------------
        Draw(ReleaseApp, Companies.IsReleaseableApp(company, Q));

        RenderInvestmentsButton();
    }

    void RenderInvestmentsButton()
    {
        bool hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);
        var playerCanExploreAdvancedTabs = hasReleasedProducts;

        bool bankruptcyLooming = TutorialUtils.IsOpenedFunctionality(Q, TutorialFunctionality.BankruptcyWarning);

        var canRaiseInvestments = playerCanExploreAdvancedTabs || bankruptcyLooming;
        Draw(RaiseInvestments, canRaiseInvestments);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }
}
