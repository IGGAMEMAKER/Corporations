using Assets.Core;
using UnityEngine.UI;

public class RenderCompanyName : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var company = SelectedCompany;
        bool isRelatedToPlayer = Companies.IsRelatedToPlayer(Q, company);

        var color = isRelatedToPlayer ? Colors.COLOR_COMPANY_WHERE_I_AM_CEO : Colors.COLOR_COMPANY_WHERE_I_AM_NOT_CEO;
        

        var status = Enums.GetFormattedCompanyType(company.company.CompanyType);

        return Visuals.Colorize(company.company.Name, color) + $" ({status})";
    }

    //void OnEnable()
    //{
    //    GetComponent<Text>().text = SelectedCompany.company.Name;

    //    bool isRelatedToPlayer = Companies.IsRelatedToPlayer(Q, SelectedCompany);
    //    GetComponent<Text>().color = Visuals.GetColorFromString(isRelatedToPlayer ? Colors.COLOR_COMPANY_WHERE_I_AM_CEO : Colors.COLOR_COMPANY_WHERE_I_AM_NOT_CEO);
    //}
}
