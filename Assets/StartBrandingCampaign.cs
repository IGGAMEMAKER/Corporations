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

    public override bool IsInteractable()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        var cost = MarketingUtils.GetBrandingCampaignCost(company, GameContext);
        return !HasActiveTimer() && Companies.IsEnoughResources(MyCompany, cost);
    }

    public override string StandardTitle()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        var clients = MarketingUtils.GetAudienceGrowth(company, GameContext);
        var branding = Balance.BRAND_CAMPAIGN_BRAND_POWER_GAIN;

        return $"Start Branding Campaign\n(+{branding} Brand and +{Format.Minify(clients)} clients)";
    }


    public override CompanyTask GetCompanyTask() => new CompanyTaskBrandingCampaign(CompanyId);
    public override string ShortTitle() => "Branding Campaign";

    public void SetCompanyId(int companyId)
    {
        CompanyId = companyId;

        ViewRender();
    }
}
