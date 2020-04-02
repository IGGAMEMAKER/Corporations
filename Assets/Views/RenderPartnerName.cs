using Assets.Core;
using UnityEngine.UI;

public class RenderPartnerName : View
{
    int companyId;

    public void SetCompanyId(int companyId)
    {
        this.companyId = companyId;

        var c = Companies.Get(Q, companyId);

        GetComponent<Text>().text = Visuals.Link(c.company.Name);
    }
}
