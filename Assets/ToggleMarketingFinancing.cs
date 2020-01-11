using Assets.Core;
using UnityEngine;

public class ToggleMarketingFinancing : ToggleButtonController
{
    int companyId;
    
    public void SetCompanyId(int companyId)
    {
        this.companyId = companyId;
    }

    public override void Execute()
    {
        var company = Companies.GetCompany(GameContext, companyId);

        Products.SetFinancing(company, Financing.Marketing, 1);
    }

    private void Start()
    {
        var company = Companies.GetCompany(GameContext, companyId);
        var financing = Economy.GetMarketingFinancing(company);

        ToggleIsChosenComponent(financing == 1);
    }
}
