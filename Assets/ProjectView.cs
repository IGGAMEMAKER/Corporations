using Assets.Utils;
using Assets.Utils.Formatting;
using Assets.Utils.Humans;
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

    void Render()
    {
        CompanyType companyType = SelectedCompany.company.CompanyType;

        CompanyTypeLabel.text = EnumUtils.GetFormattedCompanyType(companyType);
        //CompanyNameLabel.text = SelectedCompany.company.Name;

        var human = HumanUtils.GetHumanById(GameContext, SelectedCompany.cEO.HumanId).human;
        string name = human.Name + " " + human.Surname;

        CEONameLabel.text = "CEO: " + (SelectedCompany.isControlledByPlayer ? "YOU" : name);

        CompanyValuation.text = "$" + ValueFormatter.Shorten(CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id));
        CompanyProfit.text = "$" + ValueFormatter.Shorten(CompanyEconomyUtils.GetCompanyIncome(SelectedCompany, GameContext));

        bool isPublic = SelectedCompany.isPublicCompany;

        PublicityStatus.text = isPublic ? "Is public company" : "Is private company";

        CompanyGoal.text = $"Company Goal: {InvestmentUtils.GetInvestorGoal(SelectedCompany.companyGoal.InvestorGoal)}";
    }
}
