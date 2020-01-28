using Assets.Core;

public class StartRegularAdCampaign : TimedButton
{
    int CompanyId;
    public override void Execute()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        var cost = Marketing.GetTargetingCampaignCost(company, GameContext);

        Companies.SupportCompany(MyCompany, company, cost);
        Marketing.StartRegularCampaign(company, GameContext);
    }

    public override bool IsInteractable()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        var cost = Marketing.GetTargetingCampaignCost(company, GameContext);
        return !HasActiveTimer() && Companies.IsEnoughResources(MyCompany, cost);
    }

    public override string StandardTitle()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        var clients = Marketing.GetAudienceGrowth(company, GameContext);
        return $"Start Targeting Campaign\n(+{Format.Minify(clients)} clients)";
    }


    public override CompanyTask GetCompanyTask() => new CompanyTaskMarketingRegularCampaign(CompanyId);
    public override string ShortTitle() => "Targeting Campaign";

    public void SetCompanyId(int companyId)
    {
        CompanyId = companyId;

        ViewRender();
    }
}