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

    public GameObject Monetisation;
    public GameObject Monetisation2;
    public GameObject Monetisation3;

    public GameObject CreateSupportTeam;
    public GameObject CreateQATeam;
    public GameObject CreateCoreTeam;

    public GameObject QA;
    public GameObject QA2;
    public GameObject QA3;

    public GameObject BrandingCampaignCheckbox;
    public GameObject BrandingCampaignCheckbox2;
    public GameObject BrandingCampaignCheckbox3;

    public GameObject TestCampaignCheckbox;

    public ReleaseApp ReleaseApp;



    void Render(GameEntity company)
    {
        var id = company.company.Id;
        
        ReleaseApp.SetCompanyId(id);

        var marketState = Markets.GetMarketState(Q, company.product.Niche);

        var goal = company.companyGoal.InvestorGoal;

        // prerelease stuff
        // ---------------------
        Draw(ReleaseApp, Companies.IsReleaseableApp(company, Q));
        Draw(TestCampaignCheckbox, !company.isRelease);

        bool greedyMode = true;

        // goal defined stuff
        // ----------------------
        bool preReleaseOrLater = goal >= InvestorGoal.Release;

        bool QATeamCreated = Products.IsUpgradeEnabled(company, ProductUpgrade.CreateQATeam);
        bool CoreTeamCreated = Products.IsUpgradeEnabled(company, ProductUpgrade.CreateManagementTeam);
        bool SupportTeamCreated = Products.IsUpgradeEnabled(company, ProductUpgrade.CreateSupportTeam);

        Draw(SupportCheckbox, greedyMode && preReleaseOrLater && SupportTeamCreated);
        Draw(SupportCheckbox2, greedyMode && preReleaseOrLater && SupportTeamCreated);
        Draw(SupportCheckbox3, greedyMode && preReleaseOrLater && SupportTeamCreated);

        Draw(Monetisation, greedyMode && preReleaseOrLater && !CoreTeamCreated);
        Draw(Monetisation2, greedyMode && preReleaseOrLater && CoreTeamCreated && !SupportTeamCreated);
        Draw(Monetisation3, greedyMode && preReleaseOrLater && CoreTeamCreated && !QATeamCreated);

        Draw(QA, greedyMode && preReleaseOrLater && QATeamCreated);
        Draw(QA2, greedyMode && preReleaseOrLater && QATeamCreated);
        Draw(QA3, greedyMode && preReleaseOrLater && QATeamCreated);

        // release stuff
        // -------------
        Draw(TargetingCampaignCheckbox,  company.isRelease && marketState >= MarketState.Innovation);
        Draw(TargetingCampaignCheckbox2, company.isRelease && marketState >= MarketState.Trending);
        Draw(TargetingCampaignCheckbox3, company.isRelease && marketState >= MarketState.MassGrowth);

        Draw(BrandingCampaignCheckbox,  company.isRelease && marketState >= MarketState.Innovation);
        Draw(BrandingCampaignCheckbox2, company.isRelease && marketState >= MarketState.Trending);
        Draw(BrandingCampaignCheckbox3, company.isRelease && marketState >= MarketState.MassGrowth);
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
