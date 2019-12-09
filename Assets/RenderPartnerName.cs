using Assets.Utils;
using UnityEngine.UI;

public class RenderPartnerName : View
{
    int companyId;

    public void SetCompanyId(int companyId)
    {
        this.companyId = companyId;

        var c = CompanyUtils.GetCompany(GameContext, companyId);

        GetComponent<Text>().text = Visuals.Link(c.company.Name);
    }
}
