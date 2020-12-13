using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductUpgradeButtons : RoleRelatedButtons
{
    public ReleaseApp ReleaseApp;
    public GameObject ChangePositioning;

    void Render()
    {
        var company = Flagship;
        var id = company.company.Id;
        
        ReleaseApp.SetCompanyId(id);

        Draw(ReleaseApp, Companies.IsReleaseableApp(company));
        Draw(ChangePositioning, false);
    }

    void RenderInvestmentsButton()
    {
        bool hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

        bool bankruptcyLooming = TutorialUtils.IsOpenedFunctionality(Q, TutorialFunctionality.BankruptcyWarning);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }
}
