using Assets.Utils;
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
        var financing = company.financing.Financing[Financing.Marketing];

        var newFinancing = 1;

        //if (financing == 0)
        //{
        //    newFinancing = 1;
        //}
        //else
        //{
        //    newFinancing = 0;
        //}

        ProductUtils.SetFinancing(company, Financing.Marketing, newFinancing);
        //company.financing.Financing[Financing.Marketing] = newFinancing;
    }

    private void Start()
    {
        var company = Companies.GetCompany(GameContext, companyId);
        var financing = company.financing.Financing[Financing.Marketing];

        ToggleIsChosenComponent(financing == 1);
    }
}
