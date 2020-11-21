using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyMainInfoView : View
{
    public override void ViewRender()
    {
        base.ViewRender();


    }

    private void OnEnable()
    {
        Teams.UpdateTeamEfficiency(SelectedCompany, Q);
    }
}
