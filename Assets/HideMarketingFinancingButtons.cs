using Assets.Utils;

public class HideMarketingFinancingButtons : HideOnSomeCondition
{
    int companyId;

    public int companyFinancing;

    public override bool HideIf()
    {
        var company = CompanyUtils.GetCompany(GameContext, companyId);
        var financing = company.financing.Financing[Financing.Marketing];

        return financing == companyFinancing;
    }

    public void SetCompanyId(int companyId)
    {
        this.companyId = companyId;
    }
}
