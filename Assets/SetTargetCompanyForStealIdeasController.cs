using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetCompanyForStealIdeasController : View
{
    public CooldownView CooldownView;
    public StealIdeasController StealIdeasController;

    public void SetTargetCompanyForStealing(int companyId)
    {
        CooldownView.SetTargetCompanyForStealing(companyId);
        StealIdeasController.SetTargetCompanyForStealing(companyId);
    }
}
