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

    public GameObject QA;
    public GameObject QA2;
    public GameObject QA3;

    public GameObject Team3;
    public GameObject Team7;
    public GameObject Team20;
    public GameObject Team100;

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
        UpdateIfNecessary(ReleaseApp, Companies.IsReleaseableApp(company, Q));
        Draw(TestCampaignCheckbox, !company.isRelease);

        bool greedyMode = true;

        // goal defined stuff
        // ----------------------
        Draw(SupportCheckbox, greedyMode && goal >= InvestorGoal.Release);
        Draw(SupportCheckbox2, greedyMode && goal >= InvestorGoal.Release);
        Draw(SupportCheckbox3, greedyMode && goal >= InvestorGoal.Release);

        Draw(Monetisation, greedyMode && goal >= InvestorGoal.Release);
        Draw(Monetisation2, greedyMode && goal >= InvestorGoal.Release);
        Draw(Monetisation3, greedyMode && goal >= InvestorGoal.Release);

        Draw(QA, greedyMode && goal >= InvestorGoal.Release);
        Draw(QA2, greedyMode && goal >= InvestorGoal.Release);
        Draw(QA3, greedyMode && goal >= InvestorGoal.Release);

        Draw(Team3, greedyMode && goal >= InvestorGoal.Release);
        Draw(Team7, greedyMode && goal >= InvestorGoal.Release);
        Draw(Team20, greedyMode && goal >= InvestorGoal.Release);
        Draw(Team100, greedyMode && goal >= InvestorGoal.Release);

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

        var flagship = Companies.GetFlagship(Q, MyCompany);

        if (flagship == null)
            return;

        Render(flagship);
    }
}
