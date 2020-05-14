using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductUpgradeButtons : View
{
    public GameObject TargetingCampaignCheckbox;
    public GameObject TargetingCampaignCheckbox2;
    public GameObject TargetingCampaignCheckbox3;

    public GameObject SupportCheckbox;
    public GameObject SupportCheckbox2;
    public GameObject SupportCheckbox3;

    public GameObject PlatformLabel;
    public GameObject MarketingLabel;
    public GameObject DecisionsLabel;
    public GameObject SupportLabel;

    public GameObject CreateSupportTeam;
    public GameObject CreateQATeam;
    public GameObject CreateCoreTeam;

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

    public ReleaseApp ReleaseApp;

    bool CanEnable(GameEntity company, ProductUpgrade upgrade)
    {
        return Products.CanEnable(company, Q, upgrade);
    }

    void Render(GameEntity company)
    {
        var id = company.company.Id;
        
        ReleaseApp.SetCompanyId(id);

        // prerelease stuff
        // ---------------------
        Draw(ReleaseApp, Companies.IsReleaseableApp(company, Q));
        Draw(TestCampaignCheckbox, !company.isRelease);

        // goal defined stuff
        // ----------------------
        Draw(SupportCheckbox,            CanEnable(company, ProductUpgrade.Support));
        Draw(SupportCheckbox2,           CanEnable(company, ProductUpgrade.Support2));
        Draw(SupportCheckbox3,           CanEnable(company, ProductUpgrade.Support3));

        Draw(CreateCoreTeam,             CanEnable(company, ProductUpgrade.CreateManagementTeam));
        Draw(CreateSupportTeam,          CanEnable(company, ProductUpgrade.CreateSupportTeam));
        Draw(CreateQATeam,               CanEnable(company, ProductUpgrade.CreateQATeam));

        Draw(QA,                         CanEnable(company, ProductUpgrade.QA));
        Draw(QA2,                        CanEnable(company, ProductUpgrade.QA2));
        Draw(QA3,                        CanEnable(company, ProductUpgrade.QA3));

        // release stuff
        // -------------
        Draw(WebCheckbox,                CanEnable(company, ProductUpgrade.PlatformWeb));
        Draw(MobileIOSCheckbox,          CanEnable(company, ProductUpgrade.PlatformMobileIOS));
        Draw(MobileAndroidCheckbox,      CanEnable(company, ProductUpgrade.PlatformMobileAndroid));
        Draw(DesktopCheckbox,            CanEnable(company, ProductUpgrade.PlatformDesktop));

        Draw(TargetingCampaignCheckbox,  CanEnable(company, ProductUpgrade.TargetingCampaign));
        Draw(TargetingCampaignCheckbox2, CanEnable(company, ProductUpgrade.TargetingCampaign2));
        Draw(TargetingCampaignCheckbox3, CanEnable(company, ProductUpgrade.TargetingCampaign3));

        Draw(BrandingCampaignCheckbox,   CanEnable(company, ProductUpgrade.BrandCampaign));
        Draw(BrandingCampaignCheckbox2,  CanEnable(company, ProductUpgrade.BrandCampaign2));
        Draw(BrandingCampaignCheckbox3,  CanEnable(company, ProductUpgrade.BrandCampaign3));
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var c = GetComponent<SpecifyCompany>();
        if (c != null)
        {
            Render(Companies.Get(Q, c.CompanyId));
            return;
        }

        var flagship = Companies.GetFlagship(Q, MyCompany);

        if (flagship == null)
            return;

        Render(flagship);
    }
}
