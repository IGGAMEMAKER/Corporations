using Assets.Utils;
using Assets.Utils.Formatting;
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
        var cost = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

        CompanyValuation.text = Format.Money(cost);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        RenderCompanyType();

        RenderCompanyEconomy();

        CompanyGoal.text = InvestmentUtils.GetFormattedInvestorGoal(SelectedCompany.companyGoal.InvestorGoal);
    }
}
