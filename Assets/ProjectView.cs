using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class ProjectView : View
{
    public Text CompanyTypeLabel;

    public Text CompanyValuation;

    public Text CompanyGoal;

    private void OnEnable()
    {
        Render();
    }

    void RenderCompanyType()
    {
        CompanyType companyType = SelectedCompany.company.CompanyType;

        CompanyTypeLabel.text = EnumUtils.GetFormattedCompanyType(companyType);
    }

    void RenderCompanyEconomy()
    {
        CompanyValuation.text = "$" + ValueFormatter.Shorten(CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id));
        //CompanyProfit.text = "$" + ValueFormatter.Shorten(CompanyEconomyUtils.GetCompanyIncome(SelectedCompany, GameContext));
    }

    void Render()
    {
        RenderCompanyType();
        //CompanyNameLabel.text = SelectedCompany.company.Name;

        RenderCompanyEconomy();

        CompanyGoal.text = InvestmentUtils.GetFormattedInvestorGoal(SelectedCompany.companyGoal.InvestorGoal);
    }
}
