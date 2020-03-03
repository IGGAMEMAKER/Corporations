using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductUpgradeButtons : View
{
    public TestCampaignButton TestCampaignButton;

    public StartRegularAdCampaign StartRegularAdCampaign;
    public StartBrandingCampaign StartBrandingCampaign;

    public GameObject RegularCampaignCheckbox;
    public GameObject BrandingCampaignCheckbox;

    public ReleaseApp ReleaseApp;

    public UpgradeProductImprovements UpgradeChurn;
    public UpgradeProductImprovements UpgradeMonetisation;
    public LinkToHiringScreen LinkToHiringScreen;

    GameEntity company;

    public void SetEntity(GameEntity c)
    {
        company = c;

        Render();
    }

    void Render()
    {
        if (company == null)
            return;

        var id = company.company.Id;

        ReleaseApp.SetCompanyId(id);

        TestCampaignButton.SetCompanyId(id);
        StartRegularAdCampaign.SetCompanyId(id);
        StartBrandingCampaign.SetCompanyId(id);
        UpgradeChurn.SetCompanyId(id);
        UpgradeMonetisation.SetCompanyId(id);


        var max = Products.GetNecessaryAmountOfWorkers(company, Q);
        var workers = Teams.GetAmountOfWorkers(company, Q);

        var canHireTopManagers = false && workers > 5;

        var targetingCost = Marketing.GetTargetingCost(company, Q);
        var brandingCost = Marketing.GetBrandingCost(company, Q);


        // enable / disable them
        UpdateIfNecessary(ReleaseApp, Companies.IsReleaseableApp(company, Q));

        UpdateIfNecessary(TestCampaignButton, !company.isRelease);

        UpdateIfNecessary(StartRegularAdCampaign, false && company.isRelease);
        UpdateIfNecessary(RegularCampaignCheckbox, company.isRelease);

        UpdateIfNecessary(StartBrandingCampaign, false && company.isRelease);
        UpdateIfNecessary(BrandingCampaignCheckbox, company.isRelease);


        StartRegularAdCampaign.GetComponent<Hint>().SetHint($"Cost: {Format.Money(targetingCost)}");
        StartBrandingCampaign.GetComponent<Hint>().SetHint($"Cost: {Format.Money(brandingCost)}");
    }

    void UpdateIfNecessary(MonoBehaviour mb, bool condition) => UpdateIfNecessary(mb.gameObject, condition);
    void UpdateIfNecessary(GameObject go, bool condition)
    {
        if (go.activeSelf != condition)
            go.SetActive(condition);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var flagship = Companies.GetFlagship(Q, MyCompany);

        if (flagship == null)
            return;

        company = flagship;

        Render();
    }
}
