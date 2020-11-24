using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyMainInfoView : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var company = SelectedCompany;

        if (company.hasProduct)
            Teams.UpdateTeamEfficiency(SelectedCompany, Q);
    }

    private void OnEnable()
    {
        ViewRender();
    }
}
