using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayDividendsController : ButtonController
{
    int companyId;

    public override void Execute()
    {
        companyId = SelectedCompany.company.Id;

        Companies.PayDividends(Q, companyId);
    }

    public void SetDividendCompany(int CompanyId)
    {
        companyId = CompanyId;
    }
}
