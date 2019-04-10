using Assets.Utils.Formatting;
using UnityEngine.UI;

public class ProjectView : View
{
    public Text CompanyTypeLabel;
    public Text CompanyNameLabel;

    private void OnEnable()
    {
        Render();
    }

    void Render()
    {
        CompanyType companyType = SelectedCompany.company.CompanyType;

        CompanyTypeLabel.text = EnumFormattingUtils.GetFormattedCompanyType(companyType);
        CompanyNameLabel.text = SelectedCompany.company.Name;
    }
}
