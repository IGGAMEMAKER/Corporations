using Assets.Core;

public class StartBrandingCampaign : TimedButton
{
    int CompanyId;
    public override void Execute()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        MarketingUtils.StartBrandingCampaign(company, GameContext);
    }

    public override CompanyTask GetCompanyTask()
    {
        return new CompanyTaskBrandingCampaign(CompanyId);
    }

    public override bool IsInteractable()
    {
        return !HasActiveTimer();
    }

    public override string StandardTitle()
    {
        return "Start Branding Campaign";
    }
}
