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

    public void SetCompanyId(int companyId)
    {
        CompanyId = companyId;

        ViewRender();
    }

    public override bool IsInteractable()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        var cost = Economy.GetRegularCampaignCost(company, GameContext);
        return !HasActiveTimer() && Companies.IsEnoughResources(MyCompany, cost); // && MarketingUtils.IsCanStartRegularCampaign(company, GameContext);
    }

    public override string StandardTitle()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        var clients = MarketingUtils.GetAudienceGrowth(company, GameContext);
        return $"Start targeting campaign\n(+{Format.Minify(clients)} clients)";
    }

    public override CompanyTask GetCompanyTask()
    {
        return new CompanyTaskMarketingRegularCampaign(CompanyId);
    }
}