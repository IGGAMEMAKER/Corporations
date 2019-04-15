using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class ProjectView : View
{
    public Text CompanyTypeLabel;
    public Text CompanyNameLabel;

    public Text CEONameLabel;

    public Text CompanyValuation;
    public Text CompanyProfit;

    private void OnEnable()
    {
        Render();
    }

    void Render()
    {
        CompanyType companyType = SelectedCompany.company.CompanyType;

        CompanyTypeLabel.text = EnumFormattingUtils.GetFormattedCompanyType(companyType);
        CompanyNameLabel.text = SelectedCompany.company.Name;

        CEONameLabel.text = "CEO: " + (SelectedCompany.isControlledByPlayer ? "YOU" : "Sundar Pichai");

        CompanyValuation.text = "$" + ValueFormatter.Shorten(CompanyEconomyUtils.GetCompanyCost(SelectedCompany.company.Id));
        CompanyProfit.text = "$" + ValueFormatter.Shorten(CompanyEconomyUtils.GetIncome(SelectedCompany, GameContext));
    }
}
