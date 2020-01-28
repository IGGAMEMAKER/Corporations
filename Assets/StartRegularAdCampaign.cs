using Assets.Core;

public class StartRegularAdCampaign : TimedButton
{
    int CompanyId;
    public override void Execute()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        var cost = Economy.GetRegularCampaignCost(company, GameContext);

        Companies.SupportCompany(MyCompany, company, cost);
        MarketingUtils.StartRegularCampaign(company, GameContext);
    }

    public override bool IsInteractable()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        var cost = Economy.GetRegularCampaignCost(company, GameContext);
        return !HasActiveTimer() && Companies.IsEnoughResources(MyCompany, cost);
    }

    public override string StandardTitle()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        var clients = MarketingUtils.GetAudienceGrowth(company, GameContext);
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