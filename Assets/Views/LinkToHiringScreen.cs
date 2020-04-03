using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkToHiringScreen : ButtonController
{
    int CompanyId;

    public override void Execute()
    {
        Navigate(ScreenMode.EmployeeScreen, Balance.MENU_SELECTED_COMPANY, CompanyId);
    }

    public void SetCompanyId(int companyId)
    {
        CompanyId = companyId;
    }
}
