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
        UpdateIfNecessary(TestCampaignCheckbox, !company.isRelease);


        // goal defined stuff
        // ----------------------
        UpdateIfNecessary(SupportCheckbox, goal >= InvestorGoal.Release);
        UpdateIfNecessary(SupportCheckbox2, goal >= InvestorGoal.Release);
        UpdateIfNecessary(SupportCheckbox3, goal >= InvestorGoal.Release);

        UpdateIfNecessary(Monetisation, goal >= InvestorGoal.Release);
        UpdateIfNecessary(Monetisation2, goal >= InvestorGoal.Release);
        UpdateIfNecessary(Monetisation3, goal >= InvestorGoal.Release);

        UpdateIfNecessary(QA, goal >= InvestorGoal.Release);
        UpdateIfNecessary(QA2, goal >= InvestorGoal.Release);
        UpdateIfNecessary(QA3, goal >= InvestorGoal.Release);

        UpdateIfNecessary(Team3, goal >= InvestorGoal.Release);
        UpdateIfNecessary(Team7, goal >= InvestorGoal.Release);
        UpdateIfNecessary(Team20, goal >= InvestorGoal.Release);
        UpdateIfNecessary(Team100, goal >= InvestorGoal.Release);

        // release stuff
        // -------------
        UpdateIfNecessary(TargetingCampaignCheckbox,  company.isRelease && marketState >= MarketState.Innovation);
        UpdateIfNecessary(TargetingCampaignCheckbox2, company.isRelease && marketState >= MarketState.Trending);
        UpdateIfNecessary(TargetingCampaignCheckbox3, company.isRelease && marketState >= MarketState.MassGrowth);

        UpdateIfNecessary(BrandingCampaignCheckbox, company.isRelease);
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
