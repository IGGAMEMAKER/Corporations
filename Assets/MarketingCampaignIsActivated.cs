using Assets.Utils;
using UnityEngine;

public class MarketingCampaignIsActivated : View
{
    int companyId;

    public override void ViewRender()
    {
        base.ViewRender();
        Debug.Log("MarketingCampaignIsActivated View");

        GetComponent<ToggleMarketingFinancing>().SetCompanyId(companyId);
        var max = 3;
        var isChosen = CompanyUtils.GetCompanyById(GameContext, companyId).financing.Financing[Financing.Marketing] == max;

        ToggleIsChosenComponent(isChosen);
    }

    public void SetCompany(int companyId)
    {
        this.companyId = companyId;
    }
}
