using Assets.Core;
using UnityEngine.UI;

public class ProjectView : View
{
    public Text CompanyTypeLabel;

    public Text CompanyValuation;

    public Text CompanyGoal;

    void RenderCompanyType()
    {
        CompanyType companyType = SelectedCompany.company.CompanyType;

        CompanyTypeLabel.text = EnumUtils.GetFormattedCompanyType(companyType);
    }

    void RenderCompanyEconomy()
    {
        var cost = Economy.GetCompanyCost(Q, SelectedCompany.company.Id);

        CompanyValuation.text = Format.Money(cost);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        RenderCompanyType();

        RenderCompanyEconomy();

        CompanyGoal.text = Investments.GetFormattedInvestorGoal(SelectedCompany.companyGoal.InvestorGoal);
    }
}
