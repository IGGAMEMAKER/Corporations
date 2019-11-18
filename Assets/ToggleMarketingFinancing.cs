using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMarketingFinancing : ButtonController
{
    int companyId;
    public override void Execute()
    {
        Debug.Log("MarketingCampaignIsActivated ButtonController");


        var company = CompanyUtils.GetCompanyById(GameContext, companyId);
        var financing = company.financing.Financing[Financing.Marketing];

        var max = 3;
        company.financing.Financing[Financing.Marketing] = financing == max ? 0 : max;
    }

    public void SetCompanyId(int companyId)
    {
        this.companyId = companyId;
    }
}
