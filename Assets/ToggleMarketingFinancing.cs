using Assets.Utils;

public class ToggleMarketingFinancing : ToggleButtonController
{
    int companyId;
    
    public void SetCompanyId(int companyId)
    {
        this.companyId = companyId;
    }

    public override void Execute()
    {
        var company = CompanyUtils.GetCompanyById(GameContext, companyId);
        var financing = company.financing.Financing[Financing.Marketing];

        var newFinancing = 0;

        if (financing == 0)
        {
            newFinancing = 1;
        }
        else
        {
            newFinancing = 0;
        }

        ProductUtils.SetFinancing(company, Financing.Marketing, newFinancing);
        //company.financing.Financing[Financing.Marketing] = newFinancing;
    }
}
