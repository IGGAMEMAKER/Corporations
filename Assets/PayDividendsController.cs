using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayDividendsController : ButtonController
{
    int companyId;

    public override void Execute()
    {
        companyId = SelectedCompany.company.Id;

        CompanyUtils.PayDividends(GameContext, companyId);
    }

    public void SetDividendCompany(int CompanyId)
    {
        companyId = CompanyId;
    }
}
