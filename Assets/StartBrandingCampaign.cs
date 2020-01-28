using Assets.Core;

public class StartBrandingCampaign : TimedButton
{
    int CompanyId;
    public override void Execute()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        var cost = MarketingUtils.GetBrandingCampaignCost(company, GameContext);

        Companies.SupportCompany(MyCompany, company, cost);
        MarketingUtils.StartBrandingCampaign(company, GameContext);
    }

    public void SetCompanyId(int companyId)
    {
        CompanyId = companyId;

        ViewRender();
    }

    public override CompanyTask GetCompanyTask()
    {
        return new CompanyTaskBrandingCampaign(CompanyId);
    }

    public override bool IsInteractable()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        var cost = MarketingUtils.GetBrandingCampaignCost(company, GameContext);
        return !HasActiveTimer() && Companies.IsEnoughResources(MyCompany, cost);
    }

    public override string ShortTitle()
    {
        return "Branding Campaign";
    }

    public override string StandardTitle()
    {
        return "Start Branding Campaign";
    }
}
