using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class ProjectView : View
{
    public Text CompanyTypeLabel;

    public Text CEONameLabel;

    public Text CompanyValuation;
    public Text CompanyProfit;

    public Text PublicityStatus;

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

    void RenderCEO()
    {
        var human = HumanUtils.GetHumanById(GameContext, SelectedCompany.cEO.HumanId).human;
        string name = $"{human.Name} {human.Surname}";

        CEONameLabel.text = "CEO: " + (SelectedCompany.isControlledByPlayer ? "YOU" : name);
    }

    void RenderCompanyStatus()
    {
        PublicityStatus.text = SelectedCompany.isPublicCompany ? "Is public company" : "Is private company";
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

        RenderCompanyStatus();

        RenderCEO();

        RenderCompanyEconomy();

        CompanyGoal.text = $"Company Goal: {InvestmentUtils.GetFormattedInvestorGoal(SelectedCompany.companyGoal.InvestorGoal)}";
    }
}
