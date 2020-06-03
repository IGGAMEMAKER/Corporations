using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleRelatedButtons : View
{
    internal bool HasWorker(WorkerRole workerRole, GameEntity company)
    {
        return !Teams.HasFreePlaceForWorker(company, workerRole);
    }

    internal bool CanHireManager(WorkerRole role, GameEntity company)
    {
        return company.isRelease && Teams.HasFreePlaceForWorker(company, role);
    }

    internal bool CanEnable(GameEntity company, ProductUpgrade upgrade)
    {
        return Products.CanEnable(company, Q, upgrade);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship; // GetFollowableCompany();

        if (company == null)
            return;

        Render(company);
    }

    internal virtual void Render(GameEntity company) {}
}

public class ProductUpgradeButtons : RoleRelatedButtons
{
    public WorkerRole WorkerRole = WorkerRole.CEO;
    public GameObject TestCampaignCheckbox;

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
        Draw(TestCampaignCheckbox, !company.isRelease);

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
