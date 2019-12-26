using Assets.Core;
using UnityEngine.UI;

public class RenderCompanyName : View
{
    void OnEnable()
    {
        bool isRelatedToPlayer = Companies.IsRelatedToPlayer(GameContext, SelectedCompany);

        GetComponent<Text>().text = SelectedCompany.company.Name;
        GetComponent<Text>().color = Visuals.GetColorFromString(isRelatedToPlayer ? VisualConstants.COLOR_COMPANY_WHERE_I_AM_CEO : VisualConstants.COLOR_COMPANY_WHERE_I_AM_NOT_CEO);
    }
}
