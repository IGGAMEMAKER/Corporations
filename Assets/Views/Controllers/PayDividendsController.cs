using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayDividendsController : ButtonController
{
    int companyId;

    public override void Execute()
    {
        Companies.PayDividends(Q, SelectedCompany);
    }

    public void SetDividendCompany(int CompanyId)
    {
        companyId = CompanyId;
    }
}
