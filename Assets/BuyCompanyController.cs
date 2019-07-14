using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCompanyController : ButtonController
{
    public override void Execute()
    {
        long bid = GetComponent<AcquisitionButtonView>().bid;

        CompanyUtils.BuyCompany(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id, bid);
    }
}
