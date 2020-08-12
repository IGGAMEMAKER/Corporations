using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductUpgradeButtons : RoleRelatedButtons
{
    public ReleaseApp ReleaseApp;
    public GameObject RaiseInvestments;

    public GameObject CompetitorToProductToggler;

    void Render()
    {
        var company = Flagship;
        var id = company.company.Id;
        
        ReleaseApp.SetCompanyId(id);

        // prerelease stuff
        // ---------------------
        Draw(ReleaseApp, false && Companies.IsReleaseableApp(company, Q));

        RenderInvestmentsButton();
    }

    void RenderInvestmentsButton()
    {
        bool hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

        bool bankruptcyLooming = TutorialUtils.IsOpenedFunctionality(Q, TutorialFunctionality.BankruptcyWarning);

        Draw(RaiseInvestments, false && (hasReleasedProducts || bankruptcyLooming));
        Draw(CompetitorToProductToggler, false && hasReleasedProducts);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }
}
