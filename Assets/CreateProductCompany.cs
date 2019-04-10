using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateProductCompany : ButtonController
{
    public override void Execute()
    {
        NicheType nicheType = MenuUtils.GetNiche(GameContext);

        var companyID = CompanyUtils.GenerateProductCompany(GameContext, "New Company", nicheType);

        if (MyProductEntity == null)
        {
            SetMyselfAsCEO(companyID);
        }
        else if (MyGroupEntity != null)
        {
            SetToGroup(companyID);
        }
    }

    void SetToGroup(int companyID)
    {
        CompanyUtils.AttachToGroup(GameContext, MyGroupEntity.company.Id, companyID);
    }

    void SetMyselfAsCEO(int companyID)
    {
        CompanyUtils.BecomeCEO(GameContext, companyID);
    }
}
