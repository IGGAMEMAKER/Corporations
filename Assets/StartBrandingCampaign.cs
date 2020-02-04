using Assets.Core;

public class StartBrandingCampaign : TimedButton
{
    public override void Execute()
    {
        var company = Companies.Get(Q, CompanyId);

        var cost = Marketing.GetBrandingCost(company, Q);

        Companies.SupportCompany(MyCompany, company, cost);
        Marketing.StartBrandingCampaign(company, Q);
    }

    public override CompanyTask GetCompanyTask() => new CompanyTaskBrandingCampaign(CompanyId);

    public override bool IsInteractable()
    {
        var company = Companies.Get(Q, CompanyId);

        var cost = Marketing.GetBrandingCost(company, Q);

        return !HasActiveTimer() && Companies.IsEnoughResources(MyCompany, cost);
    }

    public override string StandardTitle()
    {
        var company = Companies.Get(Q, CompanyId);

        var clients = Marketing.GetAudienceGrowth(company, Q);
        var branding = Balance.BRAND_CAMPAIGN_BRAND_POWER_GAIN;

        return $"Start Branding Campaign\n(+{branding} Brand and +{Format.Minify(clients)} clients)";
    }

    public override string ShortTitle() => "Branding Campaign";
}
