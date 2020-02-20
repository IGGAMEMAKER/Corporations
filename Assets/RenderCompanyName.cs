using Assets.Core;
using UnityEngine.UI;

public class RenderCompanyName : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        bool isRelatedToPlayer = Companies.IsRelatedToPlayer(Q, SelectedCompany);

        Colorize(isRelatedToPlayer, Colors.COLOR_COMPANY_WHERE_I_AM_CEO , Colors.COLOR_COMPANY_WHERE_I_AM_NOT_CEO);

        return SelectedCompany.company.Name;
    }

    //void OnEnable()
    //{
    //    GetComponent<Text>().text = SelectedCompany.company.Name;

    //    bool isRelatedToPlayer = Companies.IsRelatedToPlayer(Q, SelectedCompany);
    //    GetComponent<Text>().color = Visuals.GetColorFromString(isRelatedToPlayer ? Colors.COLOR_COMPANY_WHERE_I_AM_CEO : Colors.COLOR_COMPANY_WHERE_I_AM_NOT_CEO);
    //}
}
