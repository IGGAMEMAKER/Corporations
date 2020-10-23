using Assets.Core;
using System.Linq;
using UnityEngine.UI;

public class ProjectView : View
{
    public Text CompanyTypeLabel;

    public Text CompanyValuation;

    public Text CompanyGoal;

    void RenderCompanyType()
    {
        CompanyType companyType = SelectedCompany.company.CompanyType;

        CompanyTypeLabel.text = Enums.GetFormattedCompanyType(companyType);
    }

    void RenderCompanyEconomy()
    {
        var cost = Economy.CostOf(SelectedCompany, Q);

        CompanyValuation.text = Format.Money(cost);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        RenderCompanyType();

        RenderCompanyEconomy();

        CompanyGoal.text = Investments.GetFormattedCompanyGoals(SelectedCompany);
    }
}
