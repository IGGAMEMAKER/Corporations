using Assets.Core;

public class StartBrandingCampaign : TimedButton
{
    int CompanyId;
    public override void Execute()
    {
        var company = Companies.Get(GameContext, CompanyId);

        var cost = Marketing.GetBrandingCampaignCost(company, GameContext);

        Companies.SupportCompany(MyCompany, company, cost);
        Marketing.StartBrandingCampaign(company, GameContext);
    }

    public override bool IsInteractable()
    {
        var company = Companies.Get(GameContext, CompanyId);

        var cost = Marketing.GetBrandingCampaignCost(company, GameContext);
        return !HasActiveTimer() && Companies.IsEnoughResources(MyCompany, cost);
    }

    public override string StandardTitle()
    {
        var company = Companies.Get(GameContext, CompanyId);

        var clients = Marketing.GetAudienceGrowth(company, GameContext);
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
