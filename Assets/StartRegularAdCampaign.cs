using Assets.Core;

public class StartRegularAdCampaign : TimedButton
{
    public override void Execute()
    {
        var company = Companies.Get(Q, CompanyId);

        var cost = Marketing.GetTargetingCampaignCost(company, Q);

        Companies.SupportCompany(MyCompany, company, cost);
        Marketing.StartTargetingCampaign(company, Q);
    }

    public override bool IsInteractable()
    {
        var company = Companies.Get(Q, CompanyId);

        var cost = Marketing.GetTargetingCampaignCost(company, Q);

        return !HasActiveTimer() && Companies.IsEnoughResources(MyCompany, cost);
    }

    public override string StandardTitle()
    {
        var company = Companies.Get(Q, CompanyId);

        var clients = Marketing.GetAudienceGrowth(company, Q);
        return $"Start Targeting Campaign\n(+{Format.Minify(clients)} clients)";
    }


    public override CompanyTask GetCompanyTask() => new CompanyTaskMarketingRegularCampaign(CompanyId);
    public override string ShortTitle() => "Targeting Campaign";
}