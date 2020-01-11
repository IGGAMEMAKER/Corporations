using Assets.Core;

public class HideMarketingFinancingButtons : HideOnSomeCondition
{
    int companyId;

    public int companyFinancing;

    public override bool HideIf()
    {
        var company = Companies.GetCompany(GameContext, companyId);
        var financing = Economy.GetMarketingFinancing(company);

        return financing == companyFinancing;
    }

    public void SetCompanyId(int companyId)
    {
        this.companyId = companyId;
    }
}
