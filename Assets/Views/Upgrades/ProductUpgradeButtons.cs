using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductUpgradeButtons : View
{
    public WorkerRole WorkerRole = WorkerRole.CEO;

    public GameObject TargetingCampaignCheckbox;
    public GameObject TargetingCampaignCheckbox2;
    public GameObject TargetingCampaignCheckbox3;

    public GameObject SupportCheckbox;
    public GameObject SupportCheckbox2;
    public GameObject SupportCheckbox3;

    public GameObject QA;
    public GameObject QA2;
    public GameObject QA3;

    public GameObject BrandingCampaignCheckbox;
    public GameObject BrandingCampaignCheckbox2;
    public GameObject BrandingCampaignCheckbox3;

    public GameObject WebCheckbox;
    public GameObject DesktopCheckbox;
    public GameObject MobileIOSCheckbox;
    public GameObject MobileAndroidCheckbox;

    public GameObject TestCampaignCheckbox;

    public GameObject[] HiringManagers;

    public ReleaseApp ReleaseApp;

    public GameObject RaiseInvestments;

    bool CanEnable(GameEntity company, ProductUpgrade upgrade)
    {
        return Products.CanEnable(company, Q, upgrade);
    }

    void Render()
    {
        var company = GetFollowableCompany();
        var id = company.company.Id;
        
        ReleaseApp.SetCompanyId(id);

        // prerelease stuff
        // ---------------------
        Draw(ReleaseApp, Companies.IsReleaseableApp(company, Q));
        Draw(TestCampaignCheckbox, !company.isRelease);

        bool isCEO            = WorkerRole == WorkerRole.CEO;
        bool isMarketingLead  = WorkerRole == WorkerRole.MarketingLead;
        bool isTeamLead       = WorkerRole == WorkerRole.TeamLead;
        bool isProductManager = WorkerRole == WorkerRole.ProductManager;
        bool isProjectManager = WorkerRole == WorkerRole.ProjectManager;

        // goal defined stuff
        // ----------------------
        Draw(SupportCheckbox,            CanEnable(company, ProductUpgrade.Support) && isTeamLead);
        Draw(SupportCheckbox2,           CanEnable(company, ProductUpgrade.Support2) && isTeamLead);
        Draw(SupportCheckbox3,           CanEnable(company, ProductUpgrade.Support3) && isTeamLead);

        Draw(QA,                         CanEnable(company, ProductUpgrade.QA) && isProductManager);
        Draw(QA2,                        CanEnable(company, ProductUpgrade.QA2) && isProductManager);
        Draw(QA3,                        CanEnable(company, ProductUpgrade.QA3) && isProductManager);

        // release stuff
        // -------------
        Draw(WebCheckbox,                CanEnable(company, ProductUpgrade.PlatformWeb) && isProductManager);
        Draw(MobileIOSCheckbox,          CanEnable(company, ProductUpgrade.PlatformMobileIOS) && isProductManager);
        Draw(MobileAndroidCheckbox,      CanEnable(company, ProductUpgrade.PlatformMobileAndroid) && isProductManager);
        Draw(DesktopCheckbox,            CanEnable(company, ProductUpgrade.PlatformDesktop) && isProductManager);

        Draw(TargetingCampaignCheckbox,  CanEnable(company, ProductUpgrade.TargetingCampaign) && isMarketingLead);
        Draw(TargetingCampaignCheckbox2, CanEnable(company, ProductUpgrade.TargetingCampaign2) && isMarketingLead);
        Draw(TargetingCampaignCheckbox3, CanEnable(company, ProductUpgrade.TargetingCampaign3) && isMarketingLead);

        Draw(BrandingCampaignCheckbox,   CanEnable(company, ProductUpgrade.BrandCampaign) && isMarketingLead);
        Draw(BrandingCampaignCheckbox2,  CanEnable(company, ProductUpgrade.BrandCampaign2) && isMarketingLead);
        Draw(BrandingCampaignCheckbox3,  CanEnable(company, ProductUpgrade.BrandCampaign3) && isMarketingLead);


        bool hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);
        var playerCanExploreAdvancedTabs = hasReleasedProducts;
        bool bankruptcyLooming = TutorialUtils.IsOpenedFunctionality(Q, TutorialFunctionality.BankruptcyWarning);

        var canRaiseInvestments = playerCanExploreAdvancedTabs || bankruptcyLooming;
        Draw(RaiseInvestments, canRaiseInvestments && isCEO);

        foreach (var manager in HiringManagers)
        {
            var role = manager.GetComponent<HireManagerByRole>().WorkerRole;

            Draw(manager, CanHireManager(role, company) && isCEO);
        }
    }

    bool CanHireManager(WorkerRole role, GameEntity company)
    {
        return company.isRelease && Teams.HasFreePlaceForWorker(company, role);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }
}
